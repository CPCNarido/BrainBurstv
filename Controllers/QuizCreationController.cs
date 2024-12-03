using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UsersApp.Data;
using UsersApp.Models;
using UsersApp.ViewModels;
using System.Text.Json;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace UsersApp.Controllers
{
    public class QuizCreationController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 8; // 2 rows * 6 items per row
        private readonly ILogger<QuizCreationController> _logger;

        public QuizCreationController(ILogger<QuizCreationController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> GetQuizScores(int quizId)
        {
            var scores = await _context.ScoreRecords
                .Where(sr => sr.QuizId == quizId)
                .Include(sr => sr.User) // Include the User entity
                .OrderByDescending(sr => sr.Score) // Sort scores from highest to lowest
                .Select(sr => new
                {
                    FullName = sr.User.FullName, // Fetch the user's full name
                    sr.Score,
                    sr.SubmissionDate
                })
                .ToListAsync();

            return Ok(scores);
        }

        public async Task<IActionResult> Quiz_Creation_Ai(int page = 1)
        {
            const int PageSize = 12; // Or set this value as appropriate for your needs
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID

            var quizzes = await _context.Quizzes
                .Where(q => q.UserId == userId) // Filter quizzes by UserId
                .Select(q => new Quiz
                {
                    QuizId = q.QuizId,
                    GradeLevel = q.GradeLevel ?? string.Empty,
                    Topic = q.Topic ?? string.Empty,
                    JsonFilePath = q.JsonFilePath ?? string.Empty,
                    CorrectAnswers = q.CorrectAnswers ?? string.Empty
                })
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var totalQuizzes = await _context.Quizzes.CountAsync(q => q.UserId == userId);
            var totalPages = (int)Math.Ceiling(totalQuizzes / (double)PageSize);

            var viewModel = new QuizCreationViewModel
            {
                Quizzes = quizzes,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Result()
        {
            return View();
        }

        public async Task<IActionResult> ViewQuizzes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID

            var quizzes = await _context.Quizzes
                .Where(q => q.UserId == userId) // Filter quizzes by UserId
                .Select(q => new Quiz
                {
                    QuizId = q.QuizId,
                    GradeLevel = q.GradeLevel ?? string.Empty,
                    Topic = q.Topic ?? string.Empty,
                    JsonFilePath = q.JsonFilePath ?? string.Empty,
                    CorrectAnswers = q.CorrectAnswers ?? string.Empty
                })
                .ToListAsync();

            return View(quizzes);
        }

public async Task<IActionResult> ViewQuizDetails(int id)
{
    var quiz = await _context.Quizzes.FindAsync(id);
    if (quiz == null)
    {
        _logger.LogError($"Quiz with ID {id} not found.");
        return NotFound();
    }

    QuizDetailsViewModel quizDetails;

    if (string.IsNullOrEmpty(quiz.JsonFilePath))
    {
        // Initialize an empty quiz if the JsonFilePath is empty
        quizDetails = new QuizDetailsViewModel
        {
            QuizId = id,
            Questions = new Dictionary<int, string>(),
            Choices = new Dictionary<int, List<string>>(),
            Timer = new Dictionary<int, int>(),
            CorrectAnswers = new Dictionary<int, int>(),
            GameCode = int.Parse(quiz.GameCode)
        };
    }
    else
    {
        var quizData = System.IO.File.ReadAllText(quiz.JsonFilePath);
        quizDetails = JsonSerializer.Deserialize<QuizDetailsViewModel>(quizData);
        quizDetails.QuizId = id; // Ensure the ID is passed to the view

        // Deserialize the CorrectAnswers from the database
        List<string> correctAnswersList;
        try
        {
            correctAnswersList = JsonSerializer.Deserialize<List<string>>(quiz.CorrectAnswers);
        }
        catch (JsonException)
        {
            var correctAnswersIntList = JsonSerializer.Deserialize<List<int>>(quiz.CorrectAnswers);
            correctAnswersList = correctAnswersIntList.Select(index => index switch
            {
                0 => "a",
                1 => "b",
                2 => "c",
                3 => "d",
                _ => ""
            }).ToList();
        }

        var correctAnswersDict = new Dictionary<int, int>();

        for (int i = 0; i < correctAnswersList.Count; i++)
        {
            correctAnswersDict[i + 1] = correctAnswersList[i] switch
            {
                "a" => 0,
                "b" => 1,
                "c" => 2,
                "d" => 3,
                _ => -1
            };
        }

        quizDetails.CorrectAnswers = correctAnswersDict;

        // Assign indexes to choices and get the index of the correct answer
        foreach (var question in quizDetails.Questions)
        {
            if (quizDetails.Choices.ContainsKey(question.Key))
            {
                for (int i = 0; i < quizDetails.Choices[question.Key].Count; i++)
                {
                    _logger.LogInformation($"Question {question.Key}, Choice {i}: {quizDetails.Choices[question.Key][i]}");
                }

                if (correctAnswersDict.ContainsKey(question.Key))
                {
                    var correctAnswerIndex = correctAnswersDict[question.Key];
                    _logger.LogInformation($"Question {question.Key}, Correct Answer Index: {correctAnswerIndex}");
                    quizDetails.CorrectAnswers[question.Key] = correctAnswerIndex;
                }
                else
                {
                    _logger.LogWarning($"Question {question.Key} does not have a correct answer assigned.");
                }
            }
            else
            {
                _logger.LogWarning($"Question {question.Key} does not have any choices assigned.");
            }
        }
    }

    // Assign the GameCode
    quizDetails.GameCode = int.Parse(quiz.GameCode);

    return View(quizDetails);
}
[HttpPost]
public async Task<IActionResult> UpdateQuiz([FromBody] QuizDetailsViewModel model)
{
    try
    {
        if (model == null)
        {
            _logger.LogError("Received null model in UpdateQuiz.");
            return BadRequest(new { message = "Invalid quiz data." });
        }

        var quiz = await _context.Quizzes.FindAsync(model.QuizId);
        if (quiz == null)
        {
            _logger.LogError($"Quiz with ID {model.QuizId} not found.");
            return NotFound(new { message = $"Quiz with ID {model.QuizId} not found." });
        }

        // Ensure the model properties are not null
        model.Questions ??= new Dictionary<int, string>();
        model.Choices ??= new Dictionary<int, List<string>>();
        model.Timer ??= new Dictionary<int, int>();
        model.CorrectAnswers ??= new Dictionary<int, int>();

        // Create a new JSON file with the updated quiz details (questions and choices)
        var updatedQuizData = new
        {
            Questions = model.Questions,
            Choices = model.Choices,
            Timer = model.Timer
        };

        var updatedJson = JsonSerializer.Serialize(updatedQuizData);
        var directoryPath = Path.Combine("wwwroot", "quizzes");
        var newFilePath = Path.Combine(directoryPath, $"{Guid.NewGuid()}.json");

        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        System.IO.File.WriteAllText(newFilePath, updatedJson);

        // Update the file path and correct answers in the database
        quiz.JsonFilePath = newFilePath;
        quiz.CorrectAnswers = JsonSerializer.Serialize(model.CorrectAnswers.Values.ToList());

        // Generate a 6-digit game code if it doesn't exist
        if (string.IsNullOrEmpty(quiz.GameCode))
        {
            quiz.GameCode = new Random().Next(100000, 999999).ToString();
        }

        // Save changes to the database
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Quiz with ID {model.QuizId} updated successfully.");
        return Ok(new { message = "Quiz updated successfully" });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while updating the quiz.");
        return StatusCode(500, new { message = "An error occurred while updating the quiz.", error = ex.Message });
    }
}

        [HttpPost]
public async Task<IActionResult> JoinQuiz(string gameCode)
{
    var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.GameCode == gameCode);
    if (quiz == null)
    {
        _logger.LogError($"Quiz with Game Code {gameCode} not found.");
        return NotFound(new { message = $"Quiz with Game Code {gameCode} not found." });
    }

    return RedirectToAction("TakeQuiz", new { id = quiz.QuizId });
}

[HttpPost]
public async Task<IActionResult> SubmitScore([FromBody] ScoreSubmissionModel model)
{
    if (model == null || model.QuizId <= 0 || model.Score < 0)
    {
        _logger.LogError("Invalid score submission data.");
        return BadRequest(new { message = "Invalid score submission data." });
    }

    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID

    var scoreRecord = new ScoreRecord
    {
        QuizId = model.QuizId,
        UserId = userId,
        Score = model.Score,
        SubmissionDate = DateTime.UtcNow
    };

    _context.ScoreRecords.Add(scoreRecord);
    await _context.SaveChangesAsync();

    _logger.LogInformation($"Score for Quiz ID {model.QuizId} submitted successfully.");
    return Ok(new { message = "Score submitted successfully" });
}

public async Task<IActionResult> TakeQuiz(int id)
{
    var quiz = await _context.Quizzes.FindAsync(id);
    if (quiz == null)
    {
        _logger.LogError($"Quiz with ID {id} not found.");
        return NotFound();
    }

    var quizData = System.IO.File.ReadAllText(quiz.JsonFilePath);
    var quizDetails = JsonSerializer.Deserialize<QuizDetailsViewModel>(quizData);
    quizDetails.QuizId = id; // Ensure the ID is passed to the view

    // Deserialize the CorrectAnswers from the database
    List<string> correctAnswersList;
    try
    {
        correctAnswersList = JsonSerializer.Deserialize<List<string>>(quiz.CorrectAnswers);
    }
    catch (JsonException)
    {
        var correctAnswersIntList = JsonSerializer.Deserialize<List<int>>(quiz.CorrectAnswers);
        correctAnswersList = correctAnswersIntList.Select(index => index switch
        {
            0 => "a",
            1 => "b",
            2 => "c",
            3 => "d",
            _ => ""
        }).ToList();
    }

    var correctAnswersDict = new Dictionary<int, int>();

    for (int i = 0; i < correctAnswersList.Count; i++)
    {
        correctAnswersDict[i + 1] = correctAnswersList[i] switch
        {
            "a" => 0,
            "b" => 1,
            "c" => 2,
            "d" => 3,
            _ => -1
        };
    }

    quizDetails.CorrectAnswers = correctAnswersDict;

    return View(quizDetails);
}
        
public async Task<IActionResult> Quiz_Creation_Manual()
{
    return View();
}

[HttpPost]
public async Task<IActionResult> CreateManualQuiz(string topic, string gradeLevel, string description)
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID

    // Create a new quiz with the provided details
    var newQuiz = new Quiz
    {
        Topic = topic,
        GradeLevel = gradeLevel,
        JsonFilePath = string.Empty, // Initially empty, will be updated later
        CorrectAnswers = string.Empty, // Initially empty, will be updated later
        UserId = userId,
        GameCode = new Random().Next(100000, 999999).ToString(), // Generate a 6-digit game code
        Created_by = "Manual",
        CreatedAt = DateTime.UtcNow // Set the CreatedAt property
    };

    _context.Quizzes.Add(newQuiz);
    await _context.SaveChangesAsync();

    // Redirect to the ViewQuizDetails view with the new quiz ID
    return RedirectToAction("ViewQuizDetails", new { id = newQuiz.QuizId });
}

    }
}
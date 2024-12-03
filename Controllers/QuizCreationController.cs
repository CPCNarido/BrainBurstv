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

    var quizData = System.IO.File.ReadAllText(quiz.JsonFilePath);
    var quizDetails = JsonSerializer.Deserialize<QuizDetailsViewModel>(quizData);
    quizDetails.QuizId = id; // Ensure the ID is passed to the view

    // Deserialize the CorrectAnswers from the database
    var correctAnswersList = JsonSerializer.Deserialize<List<string>>(quiz.CorrectAnswers);
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

        // Create a new JSON file with the updated quiz details
        var updatedQuizData = new
        {
            Questions = model.Questions,
            Choices = model.Choices,
            Timer = model.Timer,
            CorrectAnswers = model.CorrectAnswers
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

        // Update the file path in the database
        quiz.JsonFilePath = newFilePath;

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

        public async Task<IActionResult> TakeQuiz(int id)
        {
            try
            {
                // Attempt to find the quiz in the database
                var quiz = await _context.Quizzes.FindAsync(id);
                if (quiz == null)
                {
                    _logger.LogError($"Quiz with ID {id} not found.");
                    return NotFound($"Quiz with ID {id} not found.");
                }

                // Read quiz data from the file
                var quizData = System.IO.File.ReadAllText(quiz.JsonFilePath);
                
                // Deserialize the quiz data into the view model
                var quizDetails = JsonSerializer.Deserialize<QuizDetailsViewModel>(quizData);
                if (quizDetails == null)
                {
                    _logger.LogError($"Failed to deserialize quiz data from file: {quiz.JsonFilePath}");
                    return BadRequest("Failed to load quiz details.");
                }

                // Deserialize the CorrectAnswers from the database
                var correctAnswersList = JsonSerializer.Deserialize<List<string>>(quiz.CorrectAnswers);
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

                // Successfully loaded quiz details, assign the quiz ID
                quizDetails.QuizId = id;

                return View(quizDetails);
            }
            catch (FileNotFoundException ex)
            {
                // Log specific error if the quiz JSON file is not found
                _logger.LogError(ex, $"Quiz JSON file not found at the path: {ex.FileName}");
                return NotFound("Quiz data file not found.");
            }
            catch (JsonException ex)
            {
                // Log error if there is an issue deserializing the quiz data
                _logger.LogError(ex, "Error deserializing quiz data.");
                return BadRequest("Failed to deserialize quiz data.");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors
                _logger.LogError(ex, "An unexpected error occurred while loading the quiz.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        
        public async Task<IActionResult> Quiz_Creation_Manual()
        {
            return View();
        }  

        public async Task<IActionResult> Quiz_Creation_True_Or_False()
        {
            return View();
        }   
    }
}
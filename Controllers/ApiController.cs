using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UsersApp.Data;
using UsersApp.Models;
using System.Collections.Generic;

namespace UsersApp.Controllers
{
    public class ApiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ApiController> _logger;
        private readonly AppDbContext _context;

        public ApiController(IHttpClientFactory httpClientFactory, ILogger<ApiController> logger, AppDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _context = context;
        }

[HttpPost]
public async Task<IActionResult> GenerateContent([FromForm] string gradeLevel, [FromForm] string topic)
{
    var prompt = $@"
    Create a quiz for grade {gradeLevel} on the topic of {topic}. 
    Make each question have 4 choices and display the question followed by the choices.
    Use the following format:
    **Question 1:**
    What is the capital of France?
    Choices:
    a) Berlin
    b) Madrid
    c) Paris
    d) Rome
    Answer: c) Paris
    ";

    var requestBody = new
    {
        contents = new[]
        {
            new
            {
                role = "user",
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        },
        systemInstruction = new
        {
            role = "user",
            parts = new[]
            {
                new { text = "this is a system instruction" }
            }
        },
        generationConfig = new
        {
            temperature = 1,
            topK = 40,
            topP = 0.95,
            maxOutputTokens = 8192,
            responseMimeType = "text/plain"
        }
    };

    var client = _httpClientFactory.CreateClient();
    var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

    var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=AIzaSyDBHjHy7n50Ak50zusGAAuQapvMTjZOs9c", content);
    var responseContent = await response.Content.ReadAsStringAsync();

    // Log the response for debugging
    _logger.LogInformation($"AI Response: {responseContent}");

    // Parse the response content to extract questions, choices, and answers
    var questions = ParseQuizContent(responseContent, out var choices, out var answers);

    // Save the parsed data into a JSON file
    var questionsDict = questions.Select((q, index) => new { q, index })
                                 .ToDictionary(x => x.index + 1, x => x.q);
    var choicesDict = choices.Select((c, index) => new { c, index })
                             .ToDictionary(x => x.index + 1, x => x.c);
    var jsonFilePath = SaveQuizToJsonFile(questionsDict, choicesDict);

    // Save the JSON file path and correct answers to the database
    var quiz = new Quiz
    {
        GradeLevel = gradeLevel,
        Topic = topic,
        JsonFilePath = jsonFilePath,
        CorrectAnswers = JsonSerializer.Serialize(answers)
    };
    _context.Quizzes.Add(quiz);
    await _context.SaveChangesAsync();
    return RedirectToAction("ViewQuizzes", "QuizCreation");
}

private List<string> ParseQuizContent(string content, out List<List<string>> choices, out List<string> answers)
{
    var questions = new List<string>();
    choices = new List<List<string>>();
    answers = new List<string>();

    // Split the response into individual question blocks
    var questionBlocks = content.Split(new[] { "Question" }, StringSplitOptions.None);

    foreach (var block in questionBlocks)
    {
        if (block.Trim().Length > 0)
        {
            try
            {
                // Split the question block into question and choices
                var parts = block.Split(new[] { "Choices:" }, StringSplitOptions.None);
                if (parts.Length > 0)
                {
                    var question = parts[0].Trim(); // Get the question text
                    questions.Add(question);

                    var choicesAndAnswer = parts.Length > 1 ? parts[1] : string.Empty;
                    var choiceAnswerParts = choicesAndAnswer.Split(new[] { "Answer:" }, StringSplitOptions.None);

                    // Handle the choices
                    var choiceLines = choiceAnswerParts[0].Trim().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    var choiceList = new List<string>();

                    foreach (var line in choiceLines)
                    {
                        var trimmedLine = line.Trim();
                        if (!string.IsNullOrEmpty(trimmedLine))
                        {
                            choiceList.Add(trimmedLine);
                        }
                    }

                    // Add default choices if less than 4
                    while (choiceList.Count < 4)
                    {
                        choiceList.Add("No choice provided.");
                    }
                    choices.Add(choiceList);

                    // Handle the answer
                    var answer = choiceAnswerParts.Length > 1 ? choiceAnswerParts[1].Trim() : string.Empty;
                    if (string.IsNullOrEmpty(answer))
                    {
                        answers.Add("No answer provided.");
                    }
                    else
                    {
                        // Extract only the answer letter (e.g., "c)")
                        var answerLetter = answer.Split(')')[0].Trim();
                        answers.Add(answerLetter);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error processing block: {e}");
            }
        }
    }

    return questions;
}

private string SaveQuizToJsonFile(Dictionary<int, string> questions, Dictionary<int, List<string>> choices)
{
    var quizData = new
    {
        Questions = questions,
        Choices = choices
    };

    var json = JsonSerializer.Serialize(quizData);
    var directoryPath = Path.Combine("wwwroot", "quizzes");
    var filePath = Path.Combine(directoryPath, $"{Guid.NewGuid()}.json");

    // Ensure the directory exists
    if (!Directory.Exists(directoryPath))
    {
        Directory.CreateDirectory(directoryPath);
    }

    System.IO.File.WriteAllText(filePath, json);
    return filePath;
}

[HttpPost]
public async Task<IActionResult> GenerateContent1([FromForm] string gradeLevel, [FromForm] string topic)
{
    var prompt = $@"
    Create a quiz for grade {gradeLevel} on the topic of {topic}. 
    Make each question have 4 choices and display the question followed by the choices.
    Use the following format:
    **Question 1:**
    What is the capital of France?
    Choices:
    a) Berlin
    b) Madrid
    c) Paris
    d) Rome
    Answer: c) Paris
    ";

    var requestBody = new
    {
        contents = new[]
        {
            new
            {
                role = "user",
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        },
        systemInstruction = new
        {
            role = "user",
            parts = new[]
            {
                new { text = "this is a system instruction" }
            }
        },
        generationConfig = new
        {
            temperature = 1,
            topK = 40,
            topP = 0.95,
            maxOutputTokens = 8192,
            responseMimeType = "text/plain"
        }
    };

    var client = _httpClientFactory.CreateClient();
    var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

    var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=AIzaSyDBHjHy7n50Ak50zusGAAuQapvMTjZOs9c", content);
    var responseContent = await response.Content.ReadAsStringAsync();

    // Log the response for debugging
    _logger.LogInformation($"AI Response: {responseContent}");

    // Parse the response content to extract questions, choices, and answers
    var questions = ParseQuizContent(responseContent, out var choices, out var answers);

    // Save the parsed data into a JSON file
    var questionsDict = questions.Select((q, index) => new { q, index })
                                 .ToDictionary(x => x.index + 1, x => x.q);
    var choicesDict = choices.Select((c, index) => new { c, index })
                             .ToDictionary(x => x.index + 1, x => x.c);
    var jsonFilePath = SaveQuizToJsonFile(questionsDict, choicesDict);

    // Save the JSON file path and correct answers to the database
    var quiz = new Quiz
    {
        GradeLevel = gradeLevel,
        Topic = topic,
        JsonFilePath = jsonFilePath,
        CorrectAnswers = JsonSerializer.Serialize(answers)
    };
    _context.Quizzes.Add(quiz);
    await _context.SaveChangesAsync();
    return RedirectToAction("ViewQuizzes", "QuizCreation");
}
    }
}
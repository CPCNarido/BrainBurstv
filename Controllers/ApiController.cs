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

    // Split the response into individual questions
    var questionBlocks = content.Split(new[] { "**Question" }, StringSplitOptions.None);

    foreach (var block in questionBlocks)
    {
        if (block.Trim().Length > 0)
        {
            try
            {
                var parts = block.Split(new[] { "Choices:" }, StringSplitOptions.None);
                var question = parts[0].Trim();
                if (parts.Length > 1)
                {
                    var choiceAnswer = parts[1].Split(new[] { "Answer:" }, StringSplitOptions.None);
                    var choiceLines = choiceAnswer[0].Trim().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    var choiceList = new List<string>();
                    foreach (var line in choiceLines)
                    {
                        var trimmedLine = line.Trim();
                        if (!string.IsNullOrEmpty(trimmedLine))
                        {
                            choiceList.Add(trimmedLine);
                        }
                    }
                    if (choiceAnswer.Length > 1)
                    {
                        var answer = choiceAnswer[1].Trim();
                        answers.Add(answer);
                    }
                    else
                    {
                        answers.Add("No answer provided.");
                    }
                    choices.Add(choiceList);
                }
                else
                {
                    choices.Add(new List<string> { "No choices provided." });
                    answers.Add("No answer provided.");
                }
                questions.Add(question);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error processing block: {e}");
            }
        }
    }

    // Ensure each question has exactly 4 choices
    for (int i = 0; i < choices.Count; i++)
    {
        if (choices[i].Count != 4)
        {
            choices[i] = new List<string> { "No choices provided.", "No choices provided.", "No choices provided.", "No choices provided." };
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
    }
}
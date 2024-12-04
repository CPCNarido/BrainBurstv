using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UsersApp.Data;
using UsersApp.Models;
using UsersApp.ViewModels;
using System.Security.Claims;
using System.Text;

namespace UsersApp.Controllers
{
    public class FlashCards : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> userManager;
        private readonly ILogger<FlashCards> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public FlashCards(AppDbContext context, UserManager<Users> userManager, ILogger<FlashCards> logger, IHttpClientFactory httpClientFactory)
        {
            this.userManager = userManager;
            _logger = logger;
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> flash_card_maker_ai()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }

            return View();
        }

[HttpPost]
public async Task<IActionResult> GenerateFlashcard([FromForm] string topic, [FromForm] string gradeLevel, [FromForm] int items)
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID

    var prompt = $@"
    NOTE:ALWAYS ADD THE HAII KEYWORD AT THE END OF THE LAST ANSWER TO STOP THE GENERATION, IF THERE WAS 5 ITEMS, THE 5TH ITEM SHOULD BE THE ONE WHO WILL BE ADDED A HAII.
    Create a {items}-item flashcard for grade {gradeLevel} with the topic of {topic}. 
    Make each question have an answer.
    Use the following format:
    **Question 1:**
    What is the capital of France?
    Answer: Paris (HAII)
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
    var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

    var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=AIzaSyDBHjHy7n50Ak50zusGAAuQapvMTjZOs9c", content);
    var responseContent = await response.Content.ReadAsStringAsync();

    // Log the response for debugging
    _logger.LogInformation($"AI Response: {responseContent}");

    // Parse the response content to extract questions and answers
    var questions = ParseFlashcardContent(responseContent);

    // Create a dictionary to hold the questions and answers
    var flashcardData = questions.Select(q => new
    {
        q.QuestionText,
        q.AnswerText
    }).ToList();

    // Serialize the flashcard data to JSON
    var json = JsonConvert.SerializeObject(flashcardData, Formatting.Indented);

    // Save the JSON data to a file
    var directoryPath = Path.Combine("wwwroot", "flashcards");
    if (!Directory.Exists(directoryPath))
    {
        Directory.CreateDirectory(directoryPath);
    }
    var filePath = Path.Combine(directoryPath, $"{Guid.NewGuid()}.json");
    await System.IO.File.WriteAllTextAsync(filePath, json);

    // Save the flashcard to the database
    var flashcard = new Flashcard
    {
        Title = topic,
        Description = $"Flashcard for {gradeLevel} on {topic}",
        JsonFilePath = filePath.Replace("\\", "/"), // Store the file path in the database
        CreatedBy ="Ai",
        CreatedAt = DateTime.UtcNow,
        Questions = questions,
        UserId = userId
    };

    _context.Flashcards.Add(flashcard);
    await _context.SaveChangesAsync();

    return RedirectToAction("FlashCardStudyMode", new { id = flashcard.Id });
}

public async Task<IActionResult> FlashCardStudyMode(int id)
{
    var flashcard = await _context.Flashcards
        .FirstOrDefaultAsync(f => f.Id == id);

    if (flashcard == null)
    {
        return NotFound();
    }

    // Read and deserialize the JSON file
    var jsonFilePath = flashcard.JsonFilePath;
    var jsonData = await System.IO.File.ReadAllTextAsync(jsonFilePath);
    var flashcardData = JsonConvert.DeserializeObject<List<Question>>(jsonData);

    var viewModel = new FlashCardStudyModeViewModel
    {
        Flashcard = flashcard,
        Questions = flashcardData
    };

    return View(viewModel);
}

private List<Question> ParseFlashcardContent(string responseContent)
{
    var questions = new List<Question>();

    try
    {
        // Identify the start of the actual content and ignore metadata
        var contentStartIndex = responseContent.IndexOf("**Question", StringComparison.Ordinal);
        if (contentStartIndex == -1)
        {
            // No valid content found
            return questions;
        }

        // Extract only the relevant content
        var relevantContent = responseContent.Substring(contentStartIndex);

        // Split the relevant content into question-answer pairs
        var parts = relevantContent.Split(new[] { "**Question" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
                continue;

            // Stop parsing if "HAII" is encountered
            if (part.Contains("HAII", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Parsing stopped due to keyword 'HAII'.");
                break; // Exit the loop if "HAII" is found
            }

            // Look for "Answer:" to split question and answer
            var questionAnswerParts = part.Split(new[] { "Answer:" }, StringSplitOptions.None);

            // Ensure both question and answer exist
            if (questionAnswerParts.Length >= 2)
            {
                // Extract and clean the question text
                var questionText = questionAnswerParts[0]
                    .Trim()
                    .Replace("\\n", "")
                    .Replace("\\", "")
                    .Trim();

                // Clean unnecessary prefixes (e.g., "**")
                if (questionText.StartsWith("**"))
                {
                    questionText = questionText.Substring(2).Trim();
                }

                // Extract and clean the answer text
                var answerText = questionAnswerParts[1]
                    .Trim()
                    .Replace("\\n", "")
                    .Replace("\\", "")
                    .Trim();

                // Ensure valid entries before adding
                if (!string.IsNullOrWhiteSpace(questionText) && !string.IsNullOrWhiteSpace(answerText))
                {
                    questions.Add(new Question
                    {
                        QuestionText = questionText,
                        AnswerText = answerText
                    });
                }
                else
                {
                    // Handle cases with missing data
                    _logger.LogWarning("Skipped invalid question or answer block.");
                }
            }
        }
    }
    catch (Exception e)
    {
        // Log and handle parsing errors gracefully
        _logger.LogError($"Error parsing flashcard content: {e.Message}");
    }

    return questions;
}


        public async Task<IActionResult> flash_card_maker_manual()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveFlashcard([FromForm] string title, [FromForm] string description, [FromForm] List<Question> questions)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID

            // Create a dictionary to hold the questions and answers
            var flashcardData = questions.Select(q => new
            {
                q.QuestionText,
                q.AnswerText
            }).ToList();

            // Serialize the flashcard data to JSON
            var json = JsonConvert.SerializeObject(flashcardData, Formatting.Indented);

            // Save the JSON data to a file
            var directoryPath = Path.Combine("wwwroot", "flashcards");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var filePath = Path.Combine(directoryPath, $"{Guid.NewGuid()}.json");
            await System.IO.File.WriteAllTextAsync(filePath, json);

            // Save the flashcard to the database
            var flashcard = new Flashcard
            {
                Title = title,
                Description = description,
                JsonFilePath = filePath.Replace("\\", "/"), // Store the file path in the database
                CreatedBy = "Manual",
                CreatedAt = DateTime.UtcNow,
                Questions = questions,
                UserId = userId
            };

            _context.Flashcards.Add(flashcard);
            await _context.SaveChangesAsync();

            return RedirectToAction("FlashCardStudyMode", new { id = flashcard.Id });
        }




    }
}
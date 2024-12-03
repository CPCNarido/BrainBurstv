using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UsersApp.Data;
using UsersApp.Models;
using UsersApp.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
            _context = context;
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

        public IActionResult FlashCardStudyMode()
        {
            return View();
        }


        public IActionResult Practice()
        {
            Console.WriteLine("testing");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveFlashcard(IFormCollection form)
        {
            try
            {
                // Parse JSON data from the form
                var jsonData = form["jsonData"];
                var flashcard = JsonConvert.DeserializeObject<Flashcard>(jsonData);
                if (flashcard == null)
                {
                    return BadRequest("Invalid flashcard data.");
                }

                // Save the flashcard to generate its Id
                _context.Flashcards.Add(flashcard);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Total files: {form.Files.Count}");
                foreach (var file in form.Files)
                {
                    _logger.LogInformation($"Processing file: {file.FileName}, Length: {file.Length}");
                    if (file.Length > 0)
                    {
                        // Process the file (e.g., save it to the server or database)
                        var filePath = Path.Combine("wwwroot/uploads", file.FileName);
                        _logger.LogInformation($"Saving file to: {filePath}");
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Assuming you want to store the file path in the flashcard object
                        var questionIndex = int.Parse(file.Name.Split('[')[1].Split(']')[0]);
                        _logger.LogInformation($"Initial {filePath}");
                        flashcard.Questions[questionIndex].ImageQuestionPath = filePath.Replace("\\", "/");
                        _logger.LogInformation($"Saved {flashcard.Questions[questionIndex].ImageQuestionPath}");
                    }
                }

                await _context.SaveChangesAsync();

                return Ok("Flashcard saved successfully.");
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (optional)
                // Console.WriteLine($"An error occurred while saving the entity changes: {ex.Message}");

                // Return a response indicating the error was ignored
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFlashcards(int flashcardId)
        {
            var questions = await _context.Questions
                .Where(q => q.FlashcardId == flashcardId)
                .Select(q => new
                {
                    q.Id,
                    q.QuestionText,
                    q.AnswerText
                })
                .ToListAsync();

            if (questions == null || !questions.Any())
                return NotFound("No questions found for this flashcard.");

            return Json(questions);
        }

        public IActionResult GetFlashcardQuestions(int flashcardId)
        {
            var questions = _context.Questions
                                     .Where(q => q.FlashcardId == flashcardId)
                                     .Select(q => new 
                                     {
                                         q.QuestionText,
                                         q.AnswerText
                                     })
                                     .ToList();

            return Json(questions);
        }

        [HttpGet]
        public IActionResult TestFlashcard()
        {
            var questions = _context.Questions.Take(10).ToList();
            return Json(questions); // Return JSON response
        }

        public async Task<IActionResult> TakeFlashcard(int id)
        {
            var flashcard = await _context.Flashcards.Include(f => f.Questions).FirstOrDefaultAsync(f => f.Id == id);
            if (flashcard == null)
            {
                return NotFound();
            }
            ViewData["Flashcard"] = flashcard;
            return View(flashcard);
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
            Create a {items}-item flashcard for grade {gradeLevel} with the topic of {topic}. 
            Make each question have an answer and optionally an image.
            Use the following format:
            **Question 1:**
            What is the capital of France?
            Answer: Paris
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
            var questions = ParseFlashcardContent(responseContent, out var answers);

            // Save the parsed data into a JSON file
            var flashcard = new Flashcard
            {
                Title = topic,
                Description = $"Flashcard for {gradeLevel} on {topic}",
                Questions = questions.Select((q, index) => new Question
                {
                    QuestionText = q,
                    AnswerText = answers[index]
                }).ToList()
            };

            _context.Flashcards.Add(flashcard);
            await _context.SaveChangesAsync();

            return RedirectToAction("FlashCardStudyMode", new { id = flashcard.Id });
        }

private List<string> ParseFlashcardContent(string responseContent, out List<string> answers)
{
    // Implement parsing logic to extract questions, answers, and images from the AI response
    // This is a placeholder implementation
    answers = new List<string>();
    var questions = new List<string>();

    // Example parsing logic (you need to adjust this based on the actual AI response format)
    var lines = responseContent.Split('\n');
    foreach (var line in lines)
    {
        if (line.StartsWith("Question"))
        {
            questions.Add(line.Substring(line.IndexOf(':') + 1).Trim());
        }
        else if (line.StartsWith("Answer"))
        {
            answers.Add(line.Substring(line.IndexOf(':') + 1).Trim());
        }
    }

    return questions;
}
    }
}
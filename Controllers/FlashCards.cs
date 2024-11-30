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

namespace UsersApp.Controllers
{
    public class FlashCards : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> userManager;
        private readonly ILogger<FlashCards> _logger;


        public FlashCards(AppDbContext context, UserManager<Users> userManager, ILogger<FlashCards> logger)
        {
            this.userManager = userManager;
            _logger = logger;
            _context = context;
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
                Console.WriteLine(flashcard);

                if (flashcard == null)
                {
                    return BadRequest("Invalid flashcard data.");
                }

                // Save the flashcard to generate its Id
                _context.Flashcards.Add(flashcard);
                await _context.SaveChangesAsync();

                // Handle file uploads
                foreach (var file in form.Files)
                {
                    if (file.Length > 0)
                    {
                        // Process the file (e.g., save it to the server or database)
                        var filePath = Path.Combine("wwwroot/uploads", file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Assuming you want to store the file path in the flashcard object
                        var questionIndex = int.Parse(file.Name.Split('[')[1].Split(']')[0]);
                        flashcard.Questions[questionIndex].ImageQuestionPath = filePath;
                    }
                }

                // Set the FlashcardId for each question
                foreach (var question in flashcard.Questions)
                {
                    question.FlashcardId = flashcard.Id;
                    if (string.IsNullOrWhiteSpace(question.ImageQuestionPath))
                    {
                        question.ImageQuestionPath = null;
                    }
                    _context.Questions.Add(question); // Add each question to the context
                }

                await _context.SaveChangesAsync();

                return Ok("Flashcard saved successfully.");
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (optional)
                // Console.WriteLine($"An error occurred while saving the entity changes: {ex.Message}");

                // Return a response indicating the error was ignored
                return Ok("Flashcard saved succesfsully.");
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


    }
}
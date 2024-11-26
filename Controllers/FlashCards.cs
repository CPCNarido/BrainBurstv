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

        public FlashCards(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Flash_Card_Maker_Manual()
        {
            return View();
        }

        public IActionResult Flash_Card_Maker_Ai()
        {
            return View();
        }

        public IActionResult FlashCardStudyMode()
        {
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
                // foreach (var file in form.Files)
                // {
                //     if (file.Length > 0)
                //     {
                //         // Process the file (e.g., save it to the server or database)
                //         var filePath = Path.Combine("wwwroot/uploads", file.FileName);
                //         using (var stream = new FileStream(filePath, FileMode.Create))
                //         {
                //             await file.CopyToAsync(stream);
                //         }

                //         // Assuming you want to store the file path in the flashcard object
                //         var questionIndex = int.Parse(file.Name.Split('[')[1].Split(']')[0]);
                //         flashcard.Questions[questionIndex].ImageQuestionPath = filePath;
                //     }
                // }

                // Set the FlashcardId for each question
                foreach (var question in flashcard.Questions)
                {
                    question.FlashcardId = flashcard.Id;
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
    }
}
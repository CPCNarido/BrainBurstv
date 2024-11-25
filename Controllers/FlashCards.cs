using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using UsersApp.Models;
using UsersApp.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsersApp.Controllers
{
    public class FlashCards : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public FlashCards(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
        public IActionResult SaveFlashcard([FromBody] Flashcard flashcard)
        {
            if (flashcard == null)
            {
                return BadRequest("Invalid flashcard data.");
            }

            // Save the flashcard data (e.g., to a database or a file)
            // For demonstration, we'll save it to a JSON file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "flashcards.json");
            var jsonData = JsonConvert.SerializeObject(flashcard, Formatting.Indented);

            System.IO.File.WriteAllText(filePath, jsonData);

            return Ok("Flashcard saved successfully.");
        }
    }
}
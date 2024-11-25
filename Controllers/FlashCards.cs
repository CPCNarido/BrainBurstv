using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SaveFlashcard([FromBody] Flashcard flashcard)
        {
            if (flashcard == null)
            {
                return BadRequest("Invalid flashcard data.");
            }

            _context.Flashcards.Add(flashcard);
            await _context.SaveChangesAsync();

            return Ok("Flashcard saved successfully.");
        }
    }
}
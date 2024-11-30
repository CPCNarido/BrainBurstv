using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UsersApp.Data;
using UsersApp.Models;
using UsersApp.ViewModels;
using System.Text.Json;

namespace UsersApp.Controllers
{
    public class QuizCreationController : Controller
    {
        private readonly AppDbContext _context;

        public QuizCreationController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Quiz_Creation_Ai()
        {
            return View();
        }

        public async Task<IActionResult> Result()
        {
            return View();
        }

        public async Task<IActionResult> ViewQuizzes()
        {
            var quizzes = await _context.Quizzes.ToListAsync();
            return View(quizzes);
        }

        public async Task<IActionResult> ViewQuizDetails(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            var quizData = System.IO.File.ReadAllText(quiz.JsonFilePath);
            var quizDetails = JsonSerializer.Deserialize<QuizDetailsViewModel>(quizData);

            return View(quizDetails);
        }

        public async Task<IActionResult> TakeQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            var quizData = System.IO.File.ReadAllText(quiz.JsonFilePath);
            var quizDetails = JsonSerializer.Deserialize<QuizDetailsViewModel>(quizData);

            return View(quizDetails);
        }
        
        public async Task<IActionResult> Quiz_Creation_Manual()
        {
            return View();
        }  

        public async Task<IActionResult> Quiz_Creation_True_Or_False()
        {
            return View();
        }   

        public async Task<IActionResult> Quiz_Creation_Multiple_Choice()
        {
            return View();
        }

        public async Task<IActionResult> Quiz_Creation_Fill_In_The_Blanks()
        {
            return View();
        }
    }
}
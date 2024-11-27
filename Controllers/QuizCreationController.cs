using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UsersApp.Data; // Add this line
using UsersApp.Models;
using UsersApp.ViewModels; // Add this line if you have a ViewModels namespace
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
            var quizzes = await _context.Quizzes
                .Select(q => new Quiz
                {
                    Id = q.Id,
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
                return NotFound();
            }

            var quizData = System.IO.File.ReadAllText(quiz.JsonFilePath);
            var quizDetails = JsonSerializer.Deserialize<QuizDetailsViewModel>(quizData);

            return View(quizDetails);
        }
    }
}
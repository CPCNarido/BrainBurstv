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
        private const int PageSize = 8; // 2 rows * 6 items per row

        public QuizCreationController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Quiz_Creation_Ai(int page = 1)
        {
            const int PageSize = 12; // Or set this value as appropriate for your needs
            
            var quizzes = await _context.Quizzes
                .Select(q => new Quiz
                {
                    Id = q.Id,
                    GradeLevel = q.GradeLevel ?? string.Empty,
                    Topic = q.Topic ?? string.Empty,
                    JsonFilePath = q.JsonFilePath ?? string.Empty,
                    CorrectAnswers = q.CorrectAnswers ?? string.Empty
                })
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var totalQuizzes = await _context.Quizzes.CountAsync();
            var totalPages = (int)Math.Ceiling(totalQuizzes / (double)PageSize);

            var viewModel = new QuizCreationViewModel
            {
                Quizzes = quizzes,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
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
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UsersApp.Models;
using Microsoft.AspNetCore.Identity;
using UsersApp.Data; // Add this line
using UsersApp.ViewModels; // Add this line
using Microsoft.EntityFrameworkCore; // Add this line

namespace UsersApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Users> _userManager;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<Users> userManager, AppDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }

            return View();
        }

        public async Task<IActionResult> AboutUs()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }

            return View();
        }

        public async Task<IActionResult> ContactUs()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }

            return View();
        }

        public async Task<IActionResult> Terms()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }

            return View();
        }

        public async Task<IActionResult> Join()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> JoinQuiz(string gameCode)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.GameCode == gameCode);
            if (quiz == null)
            {
                _logger.LogError($"Quiz with Game Code {gameCode} not found.");
                return NotFound(new { message = $"Quiz with Game Code {gameCode} not found." });
            }

            return RedirectToAction("TakeQuiz", new { id = quiz.QuizId });
        }

        public async Task<IActionResult> Privacy()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(ReviewViewModel model)
        {
            _logger.LogInformation("SubmitReview called");

            try
            {
                string userName = "Anonymous";
                string userRole = "Anonymous";

                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    userName = user.FullName;
                    userRole = user.Role;
                }

                var review = new Review
                {
                    UserName = userName,
                    UserRole = userRole,
                    Rating = model.Rating,
                    Feedback = model.Feedback
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Review saved successfully");

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while submitting review");
                _logger.LogError($"Exception Message: {ex.Message}");
                _logger.LogError($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = "An error occurred while submitting your review. Please try again later." });
            }
        }

        public async Task<IActionResult> Review()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
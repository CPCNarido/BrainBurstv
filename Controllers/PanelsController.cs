using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Models;
using Microsoft.EntityFrameworkCore;
using UsersApp.ViewModels;
using UsersApp.Data;
using Microsoft.AspNetCore.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsersApp.Controllers
{
    public class PanelsController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly AppDbContext _context;
        private readonly ILogger<PanelsController> _logger;
        private readonly IWebHostEnvironment _environment;

        public  PanelsController(SignInManager<Users> signInManager, UserManager<Users> userManager, AppDbContext context, ILogger<PanelsController> logger, IWebHostEnvironment environment)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _context = context;
            _logger = logger;
            _environment = environment;
        }


        public async Task<IActionResult> AdminPanel(string period = "all")
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;

                DateTime startDate = DateTime.MinValue;
                string filterPeriod = "All";
                switch (period.ToLower())
                {
                    case "today":
                        startDate = DateTime.Today;
                        filterPeriod = "Today";
                        break;
                    case "thismonth":
                        startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        filterPeriod = "This Month";
                        break;
                    case "thisyear":
                        startDate = new DateTime(DateTime.Today.Year, 1, 1);
                        filterPeriod = "This Year";
                        break;
                }

                var quizzesQuery = _context.Quizzes.AsQueryable();
                var flashcardsQuery = _context.Flashcards.AsQueryable();
                var usersQuery = userManager.Users.AsQueryable();
                var reviewsQuery = _context.Reviews.AsQueryable();

                if (startDate != DateTime.MinValue)
                {
                    quizzesQuery = quizzesQuery.Where(q => q.CreatedAt >= startDate);
                    flashcardsQuery = flashcardsQuery.Where(f => f.CreatedAt >= startDate);
                    usersQuery = usersQuery.Where(u => u.Created_At >= startDate);
                    reviewsQuery = reviewsQuery.Where(r => r.CreatedAt >= startDate);
                }

                var quizzes = await quizzesQuery
                    .Select(q => new Quiz
                    {
                        QuizId = q.QuizId,
                        GradeLevel = q.GradeLevel ?? string.Empty,
                        Topic = q.Topic ?? string.Empty,
                        CorrectAnswers = q.CorrectAnswers ?? string.Empty,
                        JsonFilePath = q.JsonFilePath ?? string.Empty,
                        UserId = q.UserId,
                        HighestScore = q.HighestScore,
                        GameCode = q.GameCode ?? string.Empty
                    })
                    .ToListAsync();

                var flashcards = await flashcardsQuery
                    .Include(f => f.Questions)
                    .ToListAsync();

                var studentRatings = await reviewsQuery
                    .Where(r => r.UserRole == "Student")
                    .ToListAsync();

                var professorRatings = await reviewsQuery
                    .Where(r => r.UserRole == "Professor")
                    .ToListAsync();

                var manualQuizCount = await quizzesQuery.CountAsync(q => q.Created_by == "Manual");
                var aiQuizCount = await quizzesQuery.CountAsync(q => q.Created_by == "Ai");
                var totalQuizCount = manualQuizCount + aiQuizCount;

                var manualFlashcardCount = await flashcardsQuery.CountAsync(f => f.CreatedBy == "Manual");
                var aiFlashcardCount = await flashcardsQuery.CountAsync(f => f.CreatedBy == "Ai");
                var totalFlashcardCount = manualFlashcardCount + aiFlashcardCount;

                var professorCount = await usersQuery.CountAsync(u => u.Role == "Professor");
                var studentCount = await usersQuery.CountAsync(u => u.Role == "Student");
                var totalUserCount = professorCount + studentCount;

                var totalContents = totalQuizCount + totalFlashcardCount;

                // Calculate average quizzes and flashcards created per month
                int months = 1;
                if (startDate != DateTime.MinValue)
                {
                    months = (DateTime.Today.Year - startDate.Year) * 12 + DateTime.Today.Month - startDate.Month + 1;
                }
                else
                {
                    var firstQuiz = await _context.Quizzes.OrderBy(q => q.CreatedAt).FirstOrDefaultAsync();
                    var firstFlashcard = await _context.Flashcards.OrderBy(f => f.CreatedAt).FirstOrDefaultAsync();
                    var firstDate = firstQuiz?.CreatedAt ?? firstFlashcard?.CreatedAt ?? DateTime.Today;
                    months = (DateTime.Today.Year - firstDate.Year) * 12 + DateTime.Today.Month - firstDate.Month + 1;
                }

                var averageQuizzesPerMonth = months > 0 ? totalQuizCount / months : 0;
                var averageFlashcardsPerMonth = months > 0 ? totalFlashcardCount / months : 0;

                ViewData["ManualQuizCount"] = manualQuizCount;
                ViewData["AiQuizCount"] = aiQuizCount;
                ViewData["TotalQuizCount"] = totalQuizCount;
                ViewData["ManualFlashcardCount"] = manualFlashcardCount;
                ViewData["AiFlashcardCount"] = aiFlashcardCount;
                ViewData["TotalFlashcardCount"] = totalFlashcardCount;
                ViewData["ProfessorCount"] = professorCount;
                ViewData["StudentCount"] = studentCount;
                ViewData["TotalUserCount"] = totalUserCount;
                ViewData["TotalContents"] = totalContents;
                ViewData["AverageQuizzesPerMonth"] = averageQuizzesPerMonth;
                ViewData["AverageFlashcardsPerMonth"] = averageFlashcardsPerMonth;
                ViewData["FilterPeriod"] = filterPeriod;

                var model = new AccountEditViewModel
                {
                    NewUsername = user.FullName,
                    Flashcards = flashcards,
                    Quizzes = quizzes,
                    StudentRatings = studentRatings,
                    ProfessorRatings = professorRatings,
                    ManualQuizCount = manualQuizCount,
                    AiQuizCount = aiQuizCount,
                    TotalQuizCount = totalQuizCount,
                    manualFlashcardCount = manualFlashcardCount,
                    aiFlashcardCount = aiFlashcardCount,
                    TotalFlashcardCount = totalFlashcardCount,
                    ProfessorCount = professorCount,
                    StudentCount = studentCount,
                    TotalUserCount = totalUserCount,
                    TotalContents = totalContents,
                    AverageQuizzesPerMonth = averageQuizzesPerMonth,
                    AverageFlashcardsPerMonth = averageFlashcardsPerMonth,
                    FilterPeriod = filterPeriod
                };

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> StudentPanel()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;
        
                var userId = user.Id; // Retrieve the user ID from the logged-in user
                var quizzes = await _context.Quizzes
                    .Where(q => q.UserId == userId) // Filter quizzes by UserId
                    .Select(q => new Quiz
                    {
                        QuizId = q.QuizId,
                        GradeLevel = q.GradeLevel ?? string.Empty,
                        Topic = q.Topic ?? string.Empty,
                        CorrectAnswers = q.CorrectAnswers ?? string.Empty,
                        JsonFilePath = q.JsonFilePath ?? string.Empty,
                        UserId = q.UserId,
                        HighestScore = q.HighestScore,
                        GameCode = q.GameCode ?? string.Empty
                    })
                    .ToListAsync();
        
                var manualQuizCount = await _context.Quizzes.CountAsync(q => q.Created_by == "Manual");
                var aiQuizCount = await _context.Quizzes.CountAsync(q => q.Created_by == "Ai");
                var TotalQuizCount = manualQuizCount + aiQuizCount;
                ViewData["TotalQuizCount"] = TotalQuizCount;
        
                var manualFlashcardCount = await _context.Flashcards.CountAsync(f => f.CreatedBy == "Manual");
                var aiFlashcardCount = await _context.Flashcards.CountAsync(f => f.CreatedBy == "Ai");
                var TotalFlashcardCount = manualFlashcardCount + aiFlashcardCount;
                ViewData["TotalFlashcardCount"] = TotalFlashcardCount;
        
                var professorCount = await userManager.Users.CountAsync(u => u.Role == "Professor");
                var studentCount = await userManager.Users.CountAsync(u => u.Role == "Student");
                var TotalUserCount = professorCount + studentCount;
                ViewData["TotalUserCount"] = TotalUserCount;
                ViewData["ProfessorCount"] = professorCount;
                ViewData["StudentCount"] = studentCount;
        
                var flashcards = await _context.Flashcards.Include(f => f.Questions).ToListAsync();
        
                var model = new AccountEditViewModel
                {
                    NewUsername = user.FullName,
                    ManualQuizCount = manualQuizCount,
                    AiQuizCount = aiQuizCount,
                    Flashcards = flashcards, // Populate the Flashcards property
                    Quizzes = quizzes // Populate the Quizzes property
                };
        
                return View(model);
            }
        
            return RedirectToAction("Login", "Account");
        }


        public async Task<IActionResult> ProfessorPanel(string period = "all")
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);
                _logger.LogInformation($"User found: {user.UserName}, FilePath: {user.FilePath}");
                ViewData["FilePath"] = user.FilePath;
                ViewData["Username"] = user.FullName;
                ViewData["Role"] = user.Role;

                DateTime startDate = DateTime.MinValue;
                string filterPeriod = "All";
                switch (period.ToLower())
                {
                    case "today":
                        startDate = DateTime.Today;
                        filterPeriod = "Today";
                        break;
                    case "thisweek":
                        startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                        filterPeriod = "This Week";
                        break;
                    case "thisyear":
                        startDate = new DateTime(DateTime.Today.Year, 1, 1);
                        filterPeriod = "This Year";
                        break;
                }

                var quizzesQuery = _context.Quizzes.AsQueryable();
                var flashcardsQuery = _context.Flashcards.AsQueryable();

                if (startDate != DateTime.MinValue)
                {
                    quizzesQuery = quizzesQuery.Where(q => q.CreatedAt >= startDate);
                    flashcardsQuery = flashcardsQuery.Where(f => f.CreatedAt >= startDate);
                }

                var quizzes = await quizzesQuery
                    .Where(q => q.UserId == user.Id)
                    .ToListAsync();

                var manualQuizCount = quizzes.Count(q => q.Created_by == "Manual");
                var aiQuizCount = quizzes.Count(q => q.Created_by == "Ai");
                var totalQuizCount = quizzes.Count;

                var flashcards = await flashcardsQuery
                    .Where(f => f.UserId == user.Id)
                    .ToListAsync();

                var manualFlashcardCount = flashcards.Count(f => f.CreatedBy == "Manual");
                var aiFlashcardCount = flashcards.Count(f => f.CreatedBy == "Ai");
                var totalFlashcardCount = flashcards.Count;

                var model = new AccountEditViewModel
                {
                    NewUsername = user.FullName,
                    ManualQuizCount = manualQuizCount,
                    AiQuizCount = aiQuizCount,
                    TotalQuizCount = totalQuizCount,
                    Flashcards = flashcards,
                    Quizzes = quizzes,
                    manualFlashcardCount = manualFlashcardCount,
                    aiFlashcardCount = aiFlashcardCount,
                    TotalFlashcardCount = totalFlashcardCount,
                    FilterPeriod = filterPeriod
                };

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> UploadProfileImageProfessor(IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                // Define the path to save the profile image in wwwroot/profile_images
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "profile_images");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save the file with a unique name to avoid conflicts
                var fileName = $"{User.Identity.Name}_{Path.GetFileName(profileImage.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save the image locally
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(fileStream);
                }

                // Update FilePath in the database with the relative path
                var user = await userManager.GetUserAsync(User);
                user.FilePath = $"/profile_images/{fileName}";  // Store relative path
                await userManager.UpdateAsync(user);
    
                return RedirectToAction("ProfessorPanel"); // Adjust redirect as needed
            }

            return View("AccountSettings"); // Return to settings page if the upload fails
        }


        [HttpPost]
        public async Task<IActionResult> UploadProfileImageStudent(IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                // Define the path to save the profile image in wwwroot/profile_images
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "profile_images");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save the file with a unique name to avoid conflicts
                var fileName = $"{User.Identity.Name}_{Path.GetFileName(profileImage.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save the image locally
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(fileStream);
                }

                // Update FilePath in the database with the relative path
                var user = await userManager.GetUserAsync(User);
                user.FilePath = $"/profile_images/{fileName}";  // Store relative path
                await userManager.UpdateAsync(user);
    
                return RedirectToAction("StudentPanel"); // Adjust redirect as needed
            }

            return View("AccountSettings"); // Return to settings page if the upload fails
        }



        //Methods for Changing Username
        [HttpPost]
        public async Task<IActionResult> AccountEditProfessor(AccountEditViewModel model)
        {
            // Retrieve the logged-in user's information
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("No user is currently logged in.");
                ModelState.AddModelError("", "No user is logged in. Please log in and try again.");
                return View("ProfessorPanel", model);
            }

            // Log the process start
            _logger.LogInformation("Starting account edit process for user ID: {UserId}", user.Id);

            // Check if the model is valid, particularly for NewUsername
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for user ID: {UserId}", user.Id);
                return View("ProfessorPanel", model);
            }

            try
            {
                // Update the FullName field with the new username
                user.FullName = model.NewUsername;
                var updateResult = await userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                {
                    // Log each error returned by UpdateAsync
                    foreach (var error in updateResult.Errors)
                    {
                        _logger.LogError("Error updating user ID: {UserId}: {Error}", user.Id ,error.Description);
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("ProfessorPanel", model);
                }

                // Log success and redirect
                _logger.LogInformation("Successfully updated account details for user ID: {UserId}", user.Id);
                TempData["SuccessMessage"] = "Account details updated successfully.";
                return RedirectToAction("ProfessorPanel", "Panels");
            }

            catch (Exception ex)
            {
                // Log any general exception
                _logger.LogError(ex, "An unexpected error occurred while updating account for user ID: {UserId}", user.Id);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
            }

            // Log that we're returning the view with errors if something failed
            _logger.LogInformation("Returning to AccountEdit view with errors for user ID: {UserId}", user.Id);
            return View("ProfessorPanel", model);
        }



        [HttpPost]
        public async Task<IActionResult> AccountEditStudent(AccountEditViewModel model)
        {
            // Retrieve the logged-in user's information
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("No user is currently logged in.");
                ModelState.AddModelError("", "No user is logged in. Please log in and try again.");
                return View("StudentPanel", model);
            }
        
            // Log the process start
            _logger.LogInformation("Starting account edit process for user ID: {UserId}", user.Id);
        
            // Check if the model is valid, particularly for NewUsername
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for user ID: {UserId}", user.Id);
                return View("StudentPanel", model);
            }
        
            try
            {
                // Update the FullName field with the new username
                user.FullName = model.NewUsername;
                var updateResult = await userManager.UpdateAsync(user);
        
                if (!updateResult.Succeeded)
                {
                    // Log each error returned by UpdateAsync
                    foreach (var error in updateResult.Errors)
                    {
                        _logger.LogError("Error updating user ID: {UserId}: {Error}", user.Id, error.Description);
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("StudentPanel", model);
                }
        
                // Log success and redirect
                _logger.LogInformation("Successfully updated account details for user ID: {UserId}", user.Id);
                TempData["SuccessMessage"] = "Account details updated successfully.";
                return RedirectToAction("StudentPanel", "Panels");
            }
            catch (Exception ex)
            {
                // Log any general exception
                _logger.LogError(ex, "An unexpected error occurred while updating account for user ID: {UserId}", user.Id);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
            }
        
            // Log that we're returning the view with errors if something failed
            _logger.LogInformation("Returning to StudentPanel view with errors for user ID: {UserId}", user.Id);
            return View("StudentPanel", model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFlashcard(int id)
        {
            var flashcard = await _context.Flashcards.FindAsync(id);
            if (flashcard == null)
            {
                return NotFound();
            }

            _context.Flashcards.Remove(flashcard);
            await _context.SaveChangesAsync();

            return Ok();
        }




            public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }

            return View("AccountEdit");
        }


    }
}
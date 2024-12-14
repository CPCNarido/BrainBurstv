﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Models;
using UsersApp.ViewModels;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using UsersApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting; // Add this
using Microsoft.AspNetCore.Http; // Add this
using System.IO; // Add this
using System.Security.Claims; // Add this

namespace UsersApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private static string otpCode;
        private readonly IWebHostEnvironment environment; // for managing file paths
        private readonly AppDbContext _context;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager, ILogger<AccountController> logger, IConfiguration configuration, IWebHostEnvironment environment, AppDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            this.environment = environment;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    ViewData["EmailError"] = "Email is required.";
                }
                else
                {
                    ViewData["EmailError"] = null; // Clear email error if field is not empty
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ViewData["PasswordError"] = "Password is required.";
                }
                else
                {
                    ViewData["PasswordError"] = null; // Clear password error if field is not empty
                }
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ViewData["EmailError"] = "Email does not match any account. Try again.";
                ViewData["PasswordError"] = null; // Clear password error
                return View(model);
            }
            else
            {
                ViewData["EmailError"] = null; // Clear email error if user is found
            }

            if (!(await userManager.CheckPasswordAsync(user, model.Password)))
            {
                ViewData["PasswordError"] = "Password is wrong. Try again.";
                ViewData["EmailError"] = null; // Clear email error
                return View(model);
            }
            else
            {
                ViewData["PasswordError"] = null; // Clear password error if password is correct
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or password is incorrect.");
                return View(model);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) // Change this line
            {
                return View(model); // Add this line to return the view if the model state is invalid
            }

            Users users = new Users
            {
                FullName = model.Name,
                Email = model.Email,
                UserName = model.Email,
                FilePath = "/profile_images/default.png",
                Role = model.Role,
                Created_At = DateTime.Now
            };

            var result = await userManager.CreateAsync(users, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        public async Task<IActionResult> VerifyEmail()
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

        private async Task SendOtpEmail(string email, string otp)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("Smtp");
                var smtpClient = new SmtpClient(smtpSettings["Host"])
                {
                    Port = int.Parse(smtpSettings["Port"]),
                    Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                    EnableSsl = bool.Parse(smtpSettings["EnableSsl"])
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["Smtp:From"], "BrainBurst Support"),
                    Subject = "BrainBurst - Password Reset OTP",
                    Body = $@"
                        <h2>Password Reset Request</h2>
                        <p>Your OTP code is: <strong>{otp}</strong></p>
                        <p>This code will expire in 5 minutes.</p>
                        <p>If you didn't request this, please ignore this email.</p>",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                _logger.LogInformation("Attempting to send email to {Email}", email);
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email sent successfully to {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}. Error: {Message}", email, ex.Message);
                throw new Exception($"Failed to send OTP email: {ex.Message}");
            }
        }

        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return View(model);
                }
                else
                {
                    var otp = GenerateOtp();
                    HttpContext.Session.SetString("OtpCode", otp);
                    await SendOtpEmail(user.Email, otp);
                    return RedirectToAction("VerifyOtp", new { username = user.UserName });
                }
            }
            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                // Define the path to save the profile image in wwwroot/profile_images
                var uploadsFolder = Path.Combine(environment.WebRootPath, "profile_images");

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
    
                return RedirectToAction("AdminPanel"); // Adjust redirect as needed
            }

            return View("AccountSettings"); // Return to settings page if the upload fails
        }
        



[HttpPost]
public async Task<IActionResult> AccountEdit(AccountEditViewModel model)
{
    // Retrieve the logged-in user's information
    var user = await userManager.GetUserAsync(User);
    if (user == null)
    {
        _logger.LogWarning("No user is currently logged in.");
        ModelState.AddModelError("", "No user is logged in. Please log in and try again.");
        return View("AccountEdit", model);
    }

    // Log the process start
    _logger.LogInformation("Starting account edit process for user ID: {UserId}", user.Id);

    // Check if the model is valid, particularly for NewUsername
    if (!ModelState.IsValid)
    {
        _logger.LogWarning("Model state is invalid for user ID: {UserId}", user.Id);
        return View("AccountEdit", model);
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
            return View("AccountEdit", model);
        }

        // Log success and redirect
        _logger.LogInformation("Successfully updated account details for user ID: {UserId}", user.Id);
        TempData["SuccessMessage"] = "Account details updated successfully.";
        return RedirectToAction("AccountEdit", "Account");
    }

    catch (Exception ex)
    {
        // Log any general exception
        _logger.LogError(ex, "An unexpected error occurred while updating account for user ID: {UserId}", user.Id);
        ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
    }

    // Log that we're returning the view with errors if something failed
    _logger.LogInformation("Returning to AccountEdit view with errors for user ID: {UserId}", user.Id);
    return View("AccountEdit", model);
}





        [HttpGet]
        public IActionResult VerifyOtp(string username)
        {
            return View(new VerifyOtpViewModel { Username = username });
        }

        [HttpPost]
        public IActionResult VerifyOtp(VerifyOtpViewModel model)
        {
            _logger.LogInformation("Verifying OTP for user: {Username}", model.Username);

            if (ModelState.IsValid)
            {
                var storedOtp = HttpContext.Session.GetString("OtpCode");
                _logger.LogInformation("Stored OTP: {StoredOtp}", storedOtp);

                if (model.Otp == storedOtp)
                {
                    _logger.LogInformation("OTP is correct. Redirecting to ChangePassword.");
                    return RedirectToAction("ChangePassword", new { username = model.Username });
                }
                else
                {
                    _logger.LogWarning("OTP is incorrect. Provided OTP: {ProvidedOtp}, Expected OTP: {ExpectedOtp}", model.Otp, storedOtp);
                    ModelState.AddModelError("", "OTP is incorrect.");
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning("Model state error: {ErrorMessage}", error.ErrorMessage);
                }
                ModelState.AddModelError("", "Model state is invalid.");
            }

            // Temporary error message for debugging
            ModelState.AddModelError("", "Failed to redirect to ChangePassword.");
            return View(model);
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

        [HttpPost]
        public async Task<IActionResult> RemoveProfileImage()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Get the current profile picture path
            var currentFilePath = user.FilePath;

            // Remove the profile picture from the database
            user.FilePath = "/profile_images/default.png";
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return StatusCode(500, "An error occurred while updating the profile picture.");
            }

            // Delete the current profile picture file from the server if it's not the default one
            if (!string.IsNullOrEmpty(currentFilePath) && currentFilePath != "/profile_images/default.png")
            {
                var filePath = Path.Combine(environment.WebRootPath, currentFilePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            return RedirectToAction("AccountSettings", "Account");
        }

        public class RemoveProfileImageRequest
        {
            public string UserId { get; set; }
        }

        [HttpPost]
        public IActionResult RemoveProfileImageStudent(string userId)
        {
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) == userId)
            {
                // Logic to remove the profile picture from the database
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    user.FilePath = "/profile_images/default.png"; // Reset to default profile image
                    _context.SaveChanges();
                    return Ok(); // Return a success response
                }
            }
            return StatusCode(500, "An error occurred while removing the profile image."); // Return an error response
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImageStudent(IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                // Define the path to save the profile image in wwwroot/profile_images
                var uploadsFolder = Path.Combine(environment.WebRootPath, "profile_images");

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

                return Ok(); // Return a success response
            }

            return StatusCode(500, "An error occurred while uploading the profile image."); // Return an error response
        }
    }
}
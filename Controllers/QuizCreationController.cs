using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Models;
using UsersApp.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsersApp.Controllers
{
    public class QuizCreationController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public QuizCreationController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Quiz_Creation_Ai()
        {
            return View();
        }
        public IActionResult Quiz_Creation_Manual()
        {
            return View();
        }

    }
}
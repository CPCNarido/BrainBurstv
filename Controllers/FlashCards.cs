using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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


    }
}
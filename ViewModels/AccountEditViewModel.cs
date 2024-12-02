using System.ComponentModel.DataAnnotations;
using UsersApp.Models;

namespace UsersApp.ViewModels
{
    public class AccountEditViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
        public string NewUsername { get; set; }

        public IEnumerable<Quiz> Quizzes { get; set; } // Add this property
        
        public int ManualQuizCount { get; set; } // Add this property
        public int AiQuizCount { get; set; } // Add this property
        public int TotalQuizCount { get; set; } // Add this property
    }

}
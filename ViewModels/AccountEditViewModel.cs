using System.ComponentModel.DataAnnotations;
using UsersApp.Models;

namespace UsersApp.ViewModels
{
    public class AccountEditViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
        public string NewUsername { get; set; }

        public List<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();
        
        public int ManualQuizCount { get; set; } // Add this property
        public int AiQuizCount { get; set; } // Add this property
        public int TotalQuizCount { get; set; } // Add this property
        public int aiFlashcardCount { get; set; } // Add this property
        public int manualFlashcardCount { get; set; } // Add this property
        public int TotalFlashcardCount { get; set; } // Add this property
        public int ProfessorCount { get; set; } // Add this property
        public int StudentCount { get; set; } // Add this property
        public int TotalUserCount { get; set; } // Add this property
        public List<Review> StudentRatings { get; set; }
        public List<Review> ProfessorRatings { get; set; }
        public List<Review> AllRatings { get; set; }
        public int TotalContents { get; set; } // Add this property
        public double AverageQuizzesPerMonth { get; set; } // Add this property
        public double AverageFlashcardsPerMonth { get; set; } // Add this property
        public double AverageRatingsPerMonth { get; set; } // Add this property
        public double AverageUsersPerMonth { get; set; } // Add this property
        public string FilterPeriod { get; set; } // Add this property
    }

}
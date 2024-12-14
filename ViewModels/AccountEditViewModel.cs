using System.ComponentModel.DataAnnotations;
using UsersApp.Models;

namespace UsersApp.ViewModels
{
    public class AccountEditViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
        public string? NewUsername { get; set; }

        public List<Flashcard>? Flashcards { get; set; } = new List<Flashcard>();
        public List<Quiz>? Quizzes { get; set; } = new List<Quiz>();

        public int? ManualQuizCount { get; set; }
        public int? AiQuizCount { get; set; }
        public int? TotalQuizCount { get; set; }
        public int? aiFlashcardCount { get; set; }
        public int? manualFlashcardCount { get; set; }
        public int? TotalFlashcardCount { get; set; }
        public int? ProfessorCount { get; set; }
        public int? StudentCount { get; set; }
        public int? TotalUserCount { get; set; }
        public List<Review>? StudentRatings { get; set; }
        public List<Review>? ProfessorRatings { get; set; }
        public List<Review>? AllRatings { get; set; }
        public int? TotalContents { get; set; }
        public double? AverageQuizzesPerMonth { get; set; }
        public double? AverageFlashcardsPerMonth { get; set; }
        public double? AverageRatingsPerMonth { get; set; }
        public double? AverageUsersPerMonth { get; set; }
        public string? FilterPeriod { get; set; }
        public string? Created_by { get; set; }

        public List<Quiz>? JoinedQuizzes { get; set; } = new List<Quiz>();
    }
}
using System.Collections.Generic;
using UsersApp.Models;

namespace UsersApp.ViewModels
{
    public class AdminPanelViewModel
    {
        public string NewUsername { get; set; }
        public List<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public List<Review> StudentRatings { get; set; } = new List<Review>();
        public List<Review> ProfessorRatings { get; set; } = new List<Review>();
    }
}
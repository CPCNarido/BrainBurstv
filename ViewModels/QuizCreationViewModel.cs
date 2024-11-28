using System.Collections.Generic;
using UsersApp.Models;

namespace UsersApp.ViewModels
{
    public class QuizCreationViewModel
    {
        public IEnumerable<Quiz> Quizzes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
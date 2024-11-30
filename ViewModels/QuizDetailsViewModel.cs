using System.Collections.Generic;

namespace UsersApp.ViewModels
{
    public class QuizDetailsViewModel
    {
        public Dictionary<int, string> Questions { get; set; }
        public Dictionary<int, List<string>> Choices { get; set; }
        public Dictionary<int, int> Timer { get; set; }
        public int Id { get; set; }
        public Dictionary<int, int> CorrectAnswers { get; set; } // Ensure this property is included
    }
}
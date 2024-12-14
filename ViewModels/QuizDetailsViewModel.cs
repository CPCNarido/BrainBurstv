using System.Collections.Generic;

namespace UsersApp.ViewModels
{
    public class QuizDetailsViewModel
    {
        public Dictionary<int, string> Questions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, List<string>> Choices { get; set; } = new Dictionary<int, List<string>>();
        public Dictionary<int, int> Timer { get; set; } = new Dictionary<int, int>();
        public int QuizId { get; set; }
        public Dictionary<int, int> CorrectAnswers { get; set; } = new Dictionary<int, int>();
        public int GameCode { get; set; } // Add GameCode property
        public string Topic { get; set; }
        public string GradeLevel { get; set; }
        public int? HighestScore  { get; set; }
    
    }
}
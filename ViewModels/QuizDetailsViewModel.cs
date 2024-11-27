using System.Collections.Generic;

namespace UsersApp.ViewModels
{
    public class QuizDetailsViewModel
    {
        public Dictionary<int, string> Questions { get; set; }
        public Dictionary<int, List<string>> Choices { get; set; }
    }
}
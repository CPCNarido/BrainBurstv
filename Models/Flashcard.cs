using System.Collections.Generic;

namespace UsersApp.Models
{
    public class Flashcard
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string Picture { get; set; }
    }
}
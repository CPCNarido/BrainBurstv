using System.Collections.Generic;

namespace UsersApp.Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string Picture { get; set; }
        public int FlashcardId { get; set; }
    }
}
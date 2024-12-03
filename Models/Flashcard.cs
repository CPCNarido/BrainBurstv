using System;
using System.Collections.Generic;

namespace UsersApp.Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string JsonFilePath { get; set; } // Add this property
        public string CreatedBy { get; set; } // Add this property
        public DateTime CreatedAt { get; set; } // Add this property
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string ImageQuestionPath { get; set; }
        public string ImageAnswerPath { get; set; } // Add this property
        public int FlashcardId { get; set; }
    }
}
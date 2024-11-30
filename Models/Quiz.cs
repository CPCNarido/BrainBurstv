namespace UsersApp.Models
{
    public class Quiz
    {
        public int QuizId { get; set; } // Change Id to QuizId
        public string GradeLevel { get; set; }
        public string Topic { get; set; }
        public string CorrectAnswers { get; set; }
        public string JsonFilePath { get; set; }
        public string UserId { get; set; } // Add UserId to reference the user
        public Users User { get; set; } // Navigation property to the User
    }
}
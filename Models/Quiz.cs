namespace UsersApp.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string GradeLevel { get; set; }
        public string Topic { get; set; }
        public string CorrectAnswers { get; set; }
        public string JsonFilePath { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }
        public string GameCode { get; set; } // Add GameCode property
    }
}
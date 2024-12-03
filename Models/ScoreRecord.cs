namespace UsersApp.Models
{
    public class ScoreRecord
    {
        public int ScoreRecordId { get; set; }
        public int QuizId { get; set; }
        public string UserId { get; set; }
        public int Score { get; set; }
        public DateTime SubmissionDate { get; set; }

        public Users User { get; set; } // Add reference to the User
    }
}
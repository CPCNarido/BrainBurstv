namespace UsersApp.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string GradeLevel { get; set; }
        public string Topic { get; set; }
        public string CorrectAnswers { get; set; }
        public string JsonFilePath { get; set; }
    }
}
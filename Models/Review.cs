using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string UserRole { get; set; }
    public int Rating { get; set; }
    public string Feedback { get; set; }
    
    public string FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
}

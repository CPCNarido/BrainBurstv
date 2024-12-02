using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string UserRole { get; set; }

    [Required]
    public int Rating { get; set; }

    public string Feedback { get; set; }
}
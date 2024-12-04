using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    public class ReviewViewModel
    {
        [Required]
        public int Rating { get; set; }

        public string Feedback { get; set; }

        public string FilePath{get; set;}
    }
}
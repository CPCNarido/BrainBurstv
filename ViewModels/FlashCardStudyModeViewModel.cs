using System.Collections.Generic;
using UsersApp.Models;

namespace UsersApp.ViewModels
{
    public class FlashCardStudyModeViewModel
    {
        public Flashcard Flashcard { get; set; }
        public List<Question> Questions { get; set; }
    }
}
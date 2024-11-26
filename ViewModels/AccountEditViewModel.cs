using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    public class AccountEditViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
        public string NewUsername { get; set; }
    }

}
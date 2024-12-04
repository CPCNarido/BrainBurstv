using Microsoft.AspNetCore.Identity;

namespace UsersApp.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; } // Add this line
        public string FilePath { get; set; }
        public DateTime Created_At { get; set; }
    }
}

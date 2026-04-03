using InventarioAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.DTOs
{
    public class RegisterDto
    {


        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8,ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.User;
    }
}

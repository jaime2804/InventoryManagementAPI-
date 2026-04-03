using InventarioAPI.Enums;

namespace InventarioAPI.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }

        public string Name { get; set; }


        public string Email { get; set; }

        public UserRole Role { get; set; } = UserRole.User;


    }
}

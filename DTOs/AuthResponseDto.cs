using InventarioAPI.Enums;

namespace InventarioAPI.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }

        public string Nombre { get; set; }


        public string Email { get; set; }

        public RolUsuario Rol { get; set; } = RolUsuario.User;


    }
}

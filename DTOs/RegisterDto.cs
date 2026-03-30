using InventarioAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.DTOs
{
    public class RegisterDto
    {


        [Required(ErrorMessage ="El nombre es obligatorio")]
        [MinLength(3, ErrorMessage ="El nombre tiene que ser de minimo 3 caracteres")]
        public string Nombre { get; set; }

        [EmailAddress(ErrorMessage ="El email es invalido")]
        [Required(ErrorMessage ="El email es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage ="La contraseña es obligatoria")]
        [MinLength(8,ErrorMessage ="La contraseña tiene que ser de minimo 8 caracteres")]
        public string Password { get; set; }

        public RolUsuario Rol { get; set; } = RolUsuario.User;
    }
}

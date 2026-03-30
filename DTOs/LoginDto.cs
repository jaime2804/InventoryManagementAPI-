using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.DTOs
{
    public class LoginDto
    {

        [EmailAddress(ErrorMessage = "El email es invalido")]
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña tiene que ser de minimo 8 caracteres")]
        public string Password { get; set; }
    }
}


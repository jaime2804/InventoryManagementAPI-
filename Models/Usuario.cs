using InventarioAPI.Enums;

namespace InventarioAPI.Models
{
    public class Usuario
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public RolUsuario Rol {  get; set; } = RolUsuario.User;


        public List<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();
    }
}

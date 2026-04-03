using InventarioAPI.Enums;

namespace InventarioAPI.Models
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public UserRole Role {  get; set; } = UserRole.User;


        public List<InventoryMovement> Movements { get; set; } = new List<InventoryMovement>();
    }
}

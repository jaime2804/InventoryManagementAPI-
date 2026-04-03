using InventarioAPI.Enums;

namespace InventarioAPI.Models
{
    public class InventoryMovement
    {
        public int Id { get; set; }

        public MovementType Type { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}

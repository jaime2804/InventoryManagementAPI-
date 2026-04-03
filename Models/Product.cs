namespace InventarioAPI.Models
{
    public class Product
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public string Description {  get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; } 

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }


        public List<InventoryMovement> Movements { get; set; } = new List<InventoryMovement>();
    }
}

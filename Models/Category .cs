namespace InventarioAPI.Models
{
    public class Category
    {

        public int Id { get; set; }

        public string Name { get; set; }


        public string Description {  get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}

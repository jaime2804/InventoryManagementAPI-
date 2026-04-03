using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0,1000000, ErrorMessage = "Price must be between 0 and 1000000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(1, 1000, ErrorMessage = "Stock must be between 0 and 100")]
        public int Stock {  get; set; }

        [Required(ErrorMessage = "Category Id is required")]
        public int CategoryId { get; set; }


    }
}

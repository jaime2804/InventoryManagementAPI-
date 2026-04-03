using InventarioAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.DTOs
{
    public class CreateMovementDto
    {

        [Required(ErrorMessage = "Movement type is required")]
        public MovementType Type { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1,100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }


    }
}

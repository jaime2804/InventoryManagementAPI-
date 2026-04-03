using InventarioAPI.Enums;

namespace InventarioAPI.DTOs
{
    public class MovementDto
    {

        public int Id { get; set; }

        public MovementType Type { get; set; }


        public int Quantity { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int UserId { get; set; }  


        public string UserName { get; set; }




    }
}

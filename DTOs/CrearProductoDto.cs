using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.DTOs
{
    public class CrearProductoDto
    {
        [Required(ErrorMessage ="El nombre es obligaotrio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es obligaotria")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligaotrio")]
        [Range(0,1000000, ErrorMessage = "El precio tiene que se entre 0 y 1000000")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La cantidad del stock es obligaotria")]
        [Range(1, 100, ErrorMessage = "El stock tiene que se entre 0 y 100")]
        public int Stock {  get; set; }

        [Required(ErrorMessage = "El Id de la categoria es obligaotria")]
        public int CategoriaId { get; set; }


    }
}

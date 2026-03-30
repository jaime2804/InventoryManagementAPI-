using InventarioAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.DTOs
{
    public class CrearMovimientoDto
    {

        [Required(ErrorMessage = "El tipo es obligaotrio")]
        public TipoMovimiento Tipo { get; set; }

        [Required(ErrorMessage = "La cantidad es obligaotrio")]
        [Range(1,100, ErrorMessage ="La cantidad debe de ser entre 1 y 100")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La descripcion es obligaotria")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Id del producto es obligaotrio")]
        public int ProductoId { get; set; }


    }
}

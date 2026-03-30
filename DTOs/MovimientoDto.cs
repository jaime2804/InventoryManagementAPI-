using InventarioAPI.Enums;

namespace InventarioAPI.DTOs
{
    public class MovimientoDto
    {

        public int Id { get; set; }

        public TipoMovimiento Tipo { get; set; }


        public int Cantidad { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int ProductoId { get; set; }

        public string NombreProducto { get; set; }

        public int UsuarioId { get; set; }  


        public string NombreUsuario { get; set; }




    }
}

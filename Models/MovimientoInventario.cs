using InventarioAPI.Enums;

namespace InventarioAPI.Models
{
    public class MovimientoInventario
    {
        public int Id { get; set; }

        public TipoMovimiento Tipo { get; set; }

        public int Cantidad { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}

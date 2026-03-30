namespace InventarioAPI.Models
{
    public class Categoria
    {

        public int Id { get; set; }

        public string Nombre { get; set; }


        public string Descripcion {  get; set; }

        public bool EstaEliminado { get; set; } = false;

        public DateTime? FechaEliminacion { get; set; }

        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}

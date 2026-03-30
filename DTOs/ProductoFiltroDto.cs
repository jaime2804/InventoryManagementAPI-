namespace InventarioAPI.DTOs
{
    public class ProductoFiltroDto : PaginacionDto
    {

        public int? CategoriaId { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public bool? StockBajo { get; set; } //productos con stock menor a 10
    }
}

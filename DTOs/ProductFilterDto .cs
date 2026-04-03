namespace InventarioAPI.DTOs
{
    public class ProductFilterDto : PaginationDto
    {

        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? LowStock { get; set; } //productos con stock menor a 10
    }
}

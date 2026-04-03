namespace InventarioAPI.DTOs
{
    public class PaginationDto
    {

        private int _size = 10;
        private const int _maxSize = 50;

        public int Page { get; set; } = 1;

        public int Size
        {
            get => _size;
            set => _size = (value > _maxSize) ? _maxSize : value;

        }

        public string? Search { get; set; }
    }
}

namespace InventarioAPI.DTOs
{
    public class PaginacionDto
    {

        private int _tamano = 10;
        private const int _tamanoMaximo = 50;

        public int Pagina { get; set; } = 1;

        public int Tamano
        {
            get => _tamano;
            set => _tamano = (value > _tamanoMaximo) ? _tamanoMaximo : value;

        }

        public string? Buscar { get; set; }
    }
}

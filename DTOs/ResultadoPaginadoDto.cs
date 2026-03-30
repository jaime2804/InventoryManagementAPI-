namespace InventarioAPI.DTOs
{
    public class ResultadoPaginadoDto<T>
    {
        public int PaginaActual { get; set; }
        public int TamanoMagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public List<T> Datos { get; set; }
    }
}

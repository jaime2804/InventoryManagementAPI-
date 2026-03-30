using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface IProductoService
    {

        Task<ResultadoPaginadoDto<ProductoDto>> ObtenerProductos(ProductoFiltroDto filtro);
        Task<ProductoDto> ObtenerProducto(int id);
        Task<ProductoDto> CrearProducto(CrearProductoDto dto);
        Task<ProductoDto> ActualizarProducto(int id, CrearProductoDto dto);
        Task<bool> EliminarProducto(int id);
    }
}

using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface ICategoriaService
    {

        Task<List<CategoriaDto>> ObtenerCategorias();
        Task<CategoriaDto> ObtenerCategoria(int id);

        Task<CategoriaDto> CrearCategoria(CategoriaDto dto);
    }
}

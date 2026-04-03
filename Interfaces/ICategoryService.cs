using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface ICategoryService
    {

        Task<List<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategory(int id);

        Task<CategoryDto> CreateCategory(CategoryDto dto);
    }
}

using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface IProductService
    {

        Task<PaginatedResultDto<ProductDto>> GetProducts(ProductFilterDto filter);
        Task<ProductDto> GetProduct(int id);
        Task<ProductDto> CreateProduct(CreateProductDto dto);
        Task<ProductDto> UpdateProduct(int id, CreateProductDto dto);
        Task<bool> DeleteProduct(int id);
    }
}

using AutoMapper;
using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace InventarioAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;


        public ProductService(AppDbContext context, IMapper mapper, ILogger<ProductService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<PaginatedResultDto<ProductDto>> GetProducts(ProductFilterDto filter)
        {
           
            var query = _context.Products.AsNoTracking().Include(p => p.Category).Where(p => !p.IsDeleted).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Search))                   
                query = query.Where(p => p.Name.Contains(filter.Search)); // Filtro de búsqueda por nombre


            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId); // Filtro por categoría

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice); // Filtro por precio mínimo


            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice); // Filtro por precio máximo


            if (filter.LowStock.HasValue && filter.LowStock.Value)
                query = query.Where(p => p.Stock < 10);                 // Filtro por stock bajo (menos de 10 unidades)

            var totalRecords = await query.CountAsync();




            var products = await query.Skip((filter.Page - 1) * filter.Size)
                                        .Take(filter.Size)
                                        .ToListAsync();

            return new PaginatedResultDto<ProductDto>
            {
                CurrentPage = filter.Page,
                PageSize = filter.Size,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / filter.Size),
                Data = _mapper.Map<List<ProductDto>>(products)
            };
        }


        public async Task<ProductDto> GetProduct(int id )
        {
            var product = await _context.Products.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync( p => p.Id == id && !p.IsDeleted);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {Id} not found", id);
                return null;
            }

            return _mapper.Map<ProductDto>(product);
        }


        public async Task<ProductDto> CreateProduct(CreateProductDto dto)
        {
            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found", dto.CategoryId);
                return null;
            }

            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product Created: {Name} with Id: {Id}", product.Name, product.Id);
            return _mapper.Map<ProductDto>(product);

        }


        public async Task<ProductDto> UpdateProduct(int id, CreateProductDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                _logger.LogWarning("Attempt to update non-existent product. ID: {Id}", id);
                return null;
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
            {
                _logger.LogWarning("Attempt to update product with non-existent category. CategoryId: {CategoryId}", dto.CategoryId);
                return null;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Product updated: {Name} with ID {Id}", product.Name, id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id ==id && !p.IsDeleted);
            if (product == null)
            {
                _logger.LogWarning("Attempt to delete non-existent product. ID: {Id}", id);
                return false;
            }
            product.IsDeleted = true; // Marcar como eliminado en lugar de eliminar físicamente
            product.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product soft deleted: {Name} with ID {Id}", product.Name, id);
            return true;
        }
    }
}

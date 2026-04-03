using AutoMapper;
using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;


        public CategoryService(AppDbContext context, IMapper mapper, ILogger<CategoryService> logger  )
        {

            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = await _context.Categories.AsNoTracking().Include(c => c.Products).Where(c => !c.IsDeleted).ToArrayAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }


        public async Task<CategoryDto> GetCategory(int id)
        {
            var category = await _context.Categories.AsNoTracking().Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (category == null)
            {
                _logger.LogWarning("Category with ID {Id} not found", id);
                return null;
            }

            return _mapper.Map<CategoryDto>(category);
        }


        public async Task<CategoryDto> CreateCategory(CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Category created with ID {Id}", category.Id);
            return _mapper.Map<CategoryDto>(category);
        }


    }
}

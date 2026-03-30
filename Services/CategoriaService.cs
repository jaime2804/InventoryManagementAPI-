using AutoMapper;
using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriaService> _logger;


        public CategoriaService(AppDbContext context, IMapper mapper, ILogger<CategoriaService> logger  )
        {

            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CategoriaDto>> ObtenerCategorias()
        {
            var categorias = await _context.Categorias.AsNoTracking().Include(c => c.Productos).Where(c => !c.EstaEliminado).ToArrayAsync();
            return _mapper.Map<List<CategoriaDto>>(categorias);
        }


        public async Task<CategoriaDto> ObtenerCategoria(int id)
        {
            var categoria = await _context.Categorias.AsNoTracking().Include(u => u.Productos).FirstOrDefaultAsync(u => u.Id == id && !u.EstaEliminado);
            if (categoria == null)
            {
                _logger.LogWarning("Categoria con ID {Id} no encontrada", id);
                return null;
            }

            return _mapper.Map<CategoriaDto>(categoria);
        }


        public async Task<CategoriaDto> CrearCategoria(CategoriaDto dto)
        {
            var categoria = _mapper.Map<Categoria>(dto);
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Categoria creada con ID {Id}", categoria.Id);
            return _mapper.Map<CategoriaDto>(categoria);
        }


    }
}

using AutoMapper;
using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventarioAPI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductoService> _logger;


        public ProductoService(AppDbContext context, IMapper mapper, ILogger<ProductoService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<ResultadoPaginadoDto<ProductoDto>> ObtenerProductos(ProductoFiltroDto filtro)
        {
           
            var query = _context.Productos.AsNoTracking().Include(p => p.Categoria).Where(p => !p.EstaEliminado).AsQueryable();

            if (!string.IsNullOrEmpty(filtro.Buscar))                   
                query = query.Where(p => p.Nombre.Contains(filtro.Buscar)); // Filtro de búsqueda por nombre


            if (filtro.CategoriaId.HasValue)
                query = query.Where(p => p.CategoriaId == filtro.CategoriaId); // Filtro por categoría

            if (filtro.PrecioMin.HasValue)
                query = query.Where(p => p.Precio >= filtro.PrecioMin); // Filtro por precio mínimo


            if (filtro.PrecioMax.HasValue)
                query = query.Where(P => P.Precio <= filtro.PrecioMax); // Filtro por precio máximo


            if (filtro.StockBajo.HasValue && filtro.StockBajo.Value)
                query = query.Where(p => p.Stock < 10);                 // Filtro por stock bajo (menos de 10 unidades)

            var totalRegistros = await query.CountAsync();




            var productos = await query.Skip((filtro.Pagina - 1) * filtro.Tamano)
                                        .Take(filtro.Tamano)
                                        .ToListAsync();

            return new ResultadoPaginadoDto<ProductoDto>
            {
                PaginaActual = filtro.Pagina,
                TamanoMagina = filtro.Tamano,
                TotalRegistros = totalRegistros,
                TotalPaginas = (int)Math.Ceiling((double)totalRegistros / filtro.Tamano),
                Datos = _mapper.Map<List<ProductoDto>>(productos)
            };
        }


        public async Task<ProductoDto> ObtenerProducto(int id )
        {
            var producto = await _context.Productos.AsNoTracking().Include(p => p.Categoria).FirstOrDefaultAsync( p => p.Id == id && !p.EstaEliminado);
            if (producto == null)
            {
                _logger.LogWarning("Producto no encontrado. Id: {Id}", id);
                return null;
            }

            return _mapper.Map<ProductoDto>(producto);
        }


        public async Task<ProductoDto> CrearProducto(CrearProductoDto dto)
        {
            var categoria = await _context.Categorias.FindAsync(dto.CategoriaId);
            if (categoria == null)
            {
                _logger.LogWarning("Intento de crear producto con categoría inexistente. CategoriaId: {CategoriaId}", dto.CategoriaId);
                return null;
            }

            var producto = _mapper.Map<Producto>(dto);
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Producto creado: {Nombre} con Id: {Id}", producto.Nombre, producto.Id);
            return _mapper.Map<ProductoDto>(producto);

        }


        public async Task<ProductoDto> ActualizarProducto(int id, CrearProductoDto dto)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            if (producto == null)
            {
                _logger.LogWarning("Intento de actualizar producto inexistente. Id: {Id}", id);
                return null;
            }

            var exiteCategoria = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
            if (!exiteCategoria)
            {
                _logger.LogWarning("Intento de actualizar producto con categoría inexistente. CategoriaId: {CategoriaId}", dto.CategoriaId);
                return null;
            }

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.CategoriaId = dto.CategoriaId;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Producto actualizado: {Nombre} con Id: {Id}", producto.Nombre, id);
            return _mapper.Map<ProductoDto>(producto);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id ==id && !p.EstaEliminado);
            if (producto == null)
            {
                _logger.LogWarning("Intento de eliminar producto inexistente. Id: {Id}", id);
                return false;
            }
            producto.EstaEliminado = true; // Marcar como eliminado en lugar de eliminar físicamente
            producto.FechaEliminacion = DateTime.Now;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Producto eliminado (Soft Delete): {Nombre} con Id: {Id}", producto.Nombre, id);
            return true;
        }
    }
}

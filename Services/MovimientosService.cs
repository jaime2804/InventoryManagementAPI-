using AutoMapper;
using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

namespace InventarioAPI.Services
{
    public class MovimientosService : IMovimientoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MovimientosService> _logger;

        public MovimientosService(AppDbContext context, IMapper mapper, ILogger<MovimientosService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<ResultadoPaginadoDto<MovimientoDto>> ObtenerMovimientos(PaginacionDto paginacion)
        {
            var query = _context.Movimientos.AsNoTracking().Include(m => m.Producto).Include(m => m.Usuario).AsQueryable();

            var totalRegistros = await query.CountAsync();

            var movimientos = await query
                .OrderByDescending(m => m.FechaCreacion)
                .Skip((paginacion.Pagina - 1) * paginacion.Tamano)
                .Take(paginacion.Tamano)
                .ToListAsync();

            return new ResultadoPaginadoDto<MovimientoDto>
            {
                PaginaActual = paginacion.Pagina,
                TamanoMagina = paginacion.Tamano,
                TotalRegistros = totalRegistros,
                TotalPaginas = (int)Math.Ceiling((double)totalRegistros / paginacion.Tamano),
                Datos = _mapper.Map<List<MovimientoDto>>(movimientos)
            };
        }

        public async Task<MovimientoDto> CrearMovimiento(CrearMovimientoDto dto, int usuarioId)
        {
            var crearMovimiento = await _context.Productos.FindAsync(dto.ProductoId);
            if (crearMovimiento == null)
            {
                _logger.LogWarning("Producto con ID {ProductoId} no encontrado para crear movimiento", dto.ProductoId);
                return null;
            }

            if(dto.Tipo == Enums.TipoMovimiento.Salida)
            {
                if (crearMovimiento.Stock < dto.Cantidad)
                    _logger.LogWarning("Stock insuficiente para producto ID {ProductoId}. Stock actual: {Stock}, Cantidad solicitada: {Cantidad}",
                        dto.ProductoId, crearMovimiento.Stock, dto.Cantidad);
                return null;              
            }

            if(dto.Tipo == Enums.TipoMovimiento.Entrada)
            {
                crearMovimiento?.Stock  += dto.Cantidad;
            } else
            {
                crearMovimiento.Stock -= dto.Cantidad;
            }
           var movimiento = _mapper.Map<MovimientoInventario>(dto);
            movimiento.UsuarioId = usuarioId;
            movimiento.FechaCreacion = DateTime.Now;
            _context.Movimientos.Add( movimiento );
            await _context.SaveChangesAsync();
            _logger.LogInformation("Movimiento creado: {Tipo} de {Cantidad} unidades para producto ID {ProductoId} por usuario ID {UsuarioId}", dto.Tipo, dto.Cantidad, dto.ProductoId, usuarioId);
            return _mapper.Map<MovimientoDto>(movimiento);
        }
    }
}

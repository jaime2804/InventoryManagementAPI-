using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface IMovimientoService
    {

        Task<ResultadoPaginadoDto<MovimientoDto>> ObtenerMovimientos(PaginacionDto paginacion);
        Task<MovimientoDto> CrearMovimiento(CrearMovimientoDto dto, int usuarioId);
    }
}

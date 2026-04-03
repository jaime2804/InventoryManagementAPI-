using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface IMovementService
    {

        Task<PaginatedResultDto<MovementDto>> GetMovements(PaginationDto pagination);
        Task<MovementDto> CreateMovement(CreateMovementDto dto, int userId);
    }
}

using AutoMapper;
using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace InventarioAPI.Services
{
    public class MovementService : IMovementService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MovementService> _logger;

        public MovementService(AppDbContext context, IMapper mapper, ILogger<MovementService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<PaginatedResultDto<MovementDto>> GetMovements(PaginationDto pagination)
        {
            var query = _context.InventoryMovements.AsNoTracking().Include(m => m.Product).Include(m => m.User).AsQueryable();

            var totalRecords = await query.CountAsync();

            var movements = await query
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pagination.Page - 1) * pagination.Size)
                .Take(pagination.Size)
                .ToListAsync();

            return new PaginatedResultDto<MovementDto>
            {
                CurrentPage = pagination.Page,
                PageSize = pagination.Size,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pagination.Size),
                Data = _mapper.Map<List<MovementDto>>(movements)
            };
        }

        public async Task<MovementDto> CreateMovement(CreateMovementDto dto, int userId)
        {
            var createMovement = await _context.Products.FindAsync(dto.ProductId);
            if (createMovement == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found to create a movement", dto.ProductId);
                return null;
            }

            if (dto.Type == Enums.MovementType.Exit)
            {
                if (createMovement.Stock < dto.Quantity)
                {


                    _logger.LogWarning("Insufficient stock for product ID {ProductId}. Current stock: {Stock}, Requested: {Quantity}",
                        dto.ProductId, createMovement.Stock, dto.Quantity);
                    return null;
                }
            }

            if(dto.Type == Enums.MovementType.Entry)
            {
                createMovement?.Stock  += dto.Quantity;
            } else
            {
                createMovement.Stock -= dto.Quantity;
            }
           var movement = _mapper.Map<InventoryMovement>(dto);
            movement.UserId = userId;
            movement.CreatedAt = DateTime.Now;
            _context.InventoryMovements.Add(movement);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Movement created: {Type} of {Quantity} units for product ID {ProductId} by user ID {UserId}", dto.Type, dto.Quantity, dto.ProductId, userId);
            return _mapper.Map<MovementDto>(movement);
        }
    }
}

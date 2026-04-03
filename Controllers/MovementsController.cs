using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventarioAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MovementsController : ControllerBase
    {

        private readonly IMovementService _service;

        public MovementsController(IMovementService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]

        public async Task<IActionResult> GetMovements([FromQuery] PaginationDto pagination)
        {
            var movements = await _service.GetMovements(pagination);
            return Ok(movements);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<IActionResult> CreateMovement([FromBody] CreateMovementDto dto)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);


            var userIdJWT = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var newMovement = await _service.CreateMovement(dto, userIdJWT);

            if (newMovement == null)
                return BadRequest("Insufficient stock or product not found");

            return CreatedAtAction(nameof(GetMovements), new { id = newMovement.Id }, newMovement);
        }

    }
}

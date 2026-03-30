using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventarioAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController :ControllerBase
    {

        private readonly IMovimientoService _service;

        public MovimientosController(IMovimientoService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]

        public async Task<IActionResult> ObtenerMovimientos([FromQuery] PaginacionDto paginacion)
        {
            var movimientos = await _service.ObtenerMovimientos(paginacion);
            return Ok(movimientos);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<IActionResult> CrearMovimiento([FromBody] CrearMovimientoDto dto)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);


            var usuarioIdJWT = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var nuevoMovimiento = await _service.CrearMovimiento(dto, usuarioIdJWT);

            if (nuevoMovimiento == null)
                return BadRequest("Stock insuficiente o producto no encontrado");

            return CreatedAtAction(nameof(ObtenerMovimientos), new { id = nuevoMovimiento.Id }, nuevoMovimiento);
        }

    }
}

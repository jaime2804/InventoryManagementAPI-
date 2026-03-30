using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventarioAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {

        private readonly IProductoService _service;


        public ProductoController(IProductoService service)
        {

            _service = service;
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> ObtenerProductos([FromQuery] ProductoFiltroDto filtro)
        {
            var productos = await _service.ObtenerProductos(filtro);
            return Ok(productos);
        }

        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> ObtenerProducto(int id )
        {
            var producto = await _service.ObtenerProducto(id);
            if (producto == null)
                return NotFound($"El producto con el Id {id} no fue encontrado");
            return Ok(producto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] CrearProductoDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);


            var nuevoProducto = await _service.CrearProducto(dto);
            if (nuevoProducto == null)
                return NotFound("La categoría no existe");

            return CreatedAtAction(nameof(ObtenerProducto), new { id = nuevoProducto.Id}, nuevoProducto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] CrearProductoDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var productoActualizado = await _service.ActualizarProducto(id, dto);
            if (productoActualizado == null) return NotFound("Producto o categoría no encontrada");
            return Ok(productoActualizado);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> EliminarProducto(int id)
        {
            var productoEliminado = await _service.EliminarProducto(id);
            if (productoEliminado == null)
                return NotFound($"El producto con el Id {id} no fue encontrado");
            return NoContent();
        }
    }
}

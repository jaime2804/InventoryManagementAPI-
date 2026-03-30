using InventarioAPI.DTOs;
using InventarioAPI.Enums;
using InventarioAPI.Interfaces;
using InventarioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase    
    {

        private readonly ICategoriaService _service;


        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> ObtenerCategorias()
        {
            var categorias = await _service.ObtenerCategorias();
            return Ok(categorias);
        }


        [Authorize]
        [HttpGet("{id}")]
        
        public async Task<IActionResult> ObtenerCategoria(int id)
        {
            var categoria = await _service.ObtenerCategoria(id);
            if(categoria == null)
                return NotFound($"La categoria con el Id {id} no fue encontrada");
            return Ok(categoria);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaDto dto )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);


            var crearCategoria = await _service.CrearCategoria(dto);
            return CreatedAtAction(nameof(ObtenerCategoria), new { id = crearCategoria.Id }, crearCategoria);
        }
    }
}

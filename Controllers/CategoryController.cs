using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase    
    {

        private readonly ICategoryService _service;


        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> GetCategories()
        {
            var categories = await _service.GetCategories();
            return Ok(categories);
        }


        [Authorize]
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _service.GetCategory(id);
            if(category == null)
                return NotFound($"Category with ID {id} not found");
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);


            var category = await _service.CreateCategory(dto);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }
    }
}

using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventarioAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _service;


        public ProductController(IProductService service)
        {

            _service = service;
        }

    
        [HttpGet]

        public async Task<IActionResult> GetProducts([FromQuery] ProductFilterDto filter)
        {
            var products = await _service.GetProducts(filter);
            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetProduct(int id )
        {
            var product = await _service.GetProduct(id);
            if (product == null)
                return NotFound($"The product with Id {id} was not found.");
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);


            var newProduct = await _service.CreateProduct(dto);
            if (newProduct == null)
                return NotFound("Category not exists");

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id}, newProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProduct = await _service.UpdateProduct(id, dto);
            if (updatedProduct == null) return NotFound("Product or Category not found");
            return Ok(updatedProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _service.DeleteProduct(id);
            if (deletedProduct == null)
            {
                return NotFound($"The product with Id {id} was not found");
            }
            return NoContent();
        }
    }
}

using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _service;

            public AuthController(IAuthService service)
        {
            _service = service;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = await _service.Register(dto);
            if (result == null)
                return BadRequest("Email address is already registered");
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.Login(dto);
            if(result == null) 
                return Unauthorized("Invalid credentials");
            return Ok(result);
        }
    }
}

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


            var resultado = await _service.Register(dto);
            if (resultado == null)
                return BadRequest("El email ya esta registrado");
            return Ok(resultado);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _service.Login(dto);
            if(resultado == null) 
                return Unauthorized("Credenciales Incorrectas");
            return Ok(resultado);
        }
    }
}

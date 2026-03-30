using AutoMapper;
using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace InventarioAPI.Services
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;


        public AuthService(AppDbContext context, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }


        public async Task<AuthResponseDto> Register( RegisterDto dto )
        {
            var existeEmail = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (existeEmail)
            {
                _logger.LogWarning("Intento de registro con email ya existente: {Email}", dto.Email);
                return null;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Rol = dto.Rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Nuevo usuario registrado con exito: {Email}", dto.Email);

            var token = GenerarToken(usuario);

            return new AuthResponseDto
            {
                Token = token,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol
            };
        }


        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null)
            {
                _logger.LogWarning("Intento de login fallido con email no registrado: {Email}", dto.Email);
                return null;
            }

            var validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            if (!validPassword)
            {
                _logger.LogWarning("Intento de login fallido con contraseña incorrecta para email: {Email}", dto.Email);
                return null;
            }


            var token = GenerarToken(usuario);


            return new AuthResponseDto
            {
                Token = token,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol
            };
        }


        private string GenerarToken(Usuario usuario) //Metodo privado que crea el JWT usa la secret key que esta en el appsettings para firmarlo 
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()), //Claims = datos que van dentro del JWT ID, EMAIL, NOMBRE, ROL
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddHours(
                double.Parse(jwtSettings["ExpirationHours"]!));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

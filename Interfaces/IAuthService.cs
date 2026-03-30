using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface IAuthService
    {

        Task<AuthResponseDto> Register(RegisterDto dto);
        Task<AuthResponseDto> Login(LoginDto dto);
    }
}

using FiapCloudGames.Application.DTOs;

namespace FiapCloudGames.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto dto);
    }
}

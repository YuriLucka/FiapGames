using FiapCloudGames.Application.DTOs;
using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Application.Interfaces
{
    public interface IJwtTokenService
    {
        TokenDto GenerateToken(User user);
    }
}

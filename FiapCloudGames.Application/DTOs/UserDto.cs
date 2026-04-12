using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Application.DTOs
{
    public record UserDto(
        int Id,
        string Name,
        string Email,
        UserRole Role,
        DateTime CreatedAt
    );
}

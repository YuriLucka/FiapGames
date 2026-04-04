using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Application.DTOs
{
    public record UserDto(
        Guid Id,
        string Name,
        string Email,
        UserRole Role,
        DateTime CreatedAt
    );
}

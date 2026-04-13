namespace FiapCloudGames.Utils.DTOs
{
    public record CreateUserDto(
        string Name,
        string Email,
        string Password
    );
}

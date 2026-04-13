namespace FiapCloudGames.Application.DTOs
{
    public record CreateGameDto(
        string Title,
        decimal Price,
        string? Description
    );
}

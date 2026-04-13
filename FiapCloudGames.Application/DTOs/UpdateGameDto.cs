namespace FiapCloudGames.Application.DTOs
{
    public record UpdateGameDto(
        string Title,
        decimal Price,
        string? Description
    );
}

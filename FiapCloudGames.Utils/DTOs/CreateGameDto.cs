namespace FiapCloudGames.Utils.DTOs
{
    public record CreateGameDto(
        string Title,
        decimal Price,
        string? Description
    );
}

namespace FiapCloudGames.Utils.DTOs
{
    public record UpdateGameDto(
        string Title,
        decimal Price,
        string? Description
    );
}

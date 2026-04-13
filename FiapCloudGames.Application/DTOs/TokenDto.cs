namespace FiapCloudGames.Application.DTOs
{
    public record TokenDto(
        string AccessToken,
        string TokenType,
        DateTime ExpiresAt
    );
}

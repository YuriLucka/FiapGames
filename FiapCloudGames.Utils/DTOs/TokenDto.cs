namespace FiapCloudGames.Utils.DTOs
{
    public record TokenDto(
        string AccessToken,
        string TokenType,
        DateTime ExpiresAt
    );
}

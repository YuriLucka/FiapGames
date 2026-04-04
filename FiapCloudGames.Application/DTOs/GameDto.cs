namespace FiapCloudGames.Application.DTOs
{
    public record GameDto(
      Guid Id,
      string Title,
      string? Description,
      decimal Price,
      bool IsActive,
      DateTime CreatedAt
  );
}

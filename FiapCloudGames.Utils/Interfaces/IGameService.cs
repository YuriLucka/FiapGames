using FiapCloudGames.Utils.DTOs;

namespace FiapCloudGames.Utils.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> GetByIdAsync(Guid id);
        Task<IEnumerable<GameDto>> GetAllAsync();
        Task<GameDto> CreateAsync(CreateGameDto dto);
        Task<GameDto> UpdateAsync(Guid id, UpdateGameDto dto);
        Task DeleteAsync(Guid id);
        Task<UserDto> AcquireGameAsync(Guid userId, Guid gameId);
    }
}

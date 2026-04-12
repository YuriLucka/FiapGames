using FiapCloudGames.Application.DTOs;

namespace FiapCloudGames.Application.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> GetByIdAsync(int id);
        Task<IEnumerable<GameDto>> GetAllAsync();
        Task<GameDto> CreateAsync(CreateGameDto dto);
        Task<GameDto> UpdateAsync(int id, UpdateGameDto dto);
        Task DeleteAsync(int id);
        Task<UserDto> AcquireGameAsync(int userId, int gameId);
    }
}

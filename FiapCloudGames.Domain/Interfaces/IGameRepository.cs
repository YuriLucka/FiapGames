using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task<Game?> GetByIdAsync(Guid id);
        Task<IEnumerable<Game>> GetAllAsync();
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(Guid id);
    }
}

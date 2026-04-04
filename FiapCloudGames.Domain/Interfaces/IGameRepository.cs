using FiapCloudGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

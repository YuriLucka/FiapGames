using FiapCloudGames.Utils.DTOs;
using FiapCloudGames.Utils.Interfaces;
using FiapGames.Web.Constants;

namespace FiapGames.Web.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient _client;

        public GameService() { }

        public GameService(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserDto> AcquireGameAsync(Guid userId, Guid gameId)
        {

            throw new NotImplementedException();

            string Endpoint = GameEndpoint.AcquireGame(userId, gameId);
            var teste = await _client.PostAsJsonAsync(Endpoint, "");
        }

        public Task<GameDto> CreateAsync(CreateGameDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GameDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GameDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GameDto> UpdateAsync(Guid id, UpdateGameDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

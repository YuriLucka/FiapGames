using FiapCloudGames.Utils.DTOs;
using FiapCloudGames.Utils.Interfaces;

namespace FiapGames.Web.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient _client;

        public GameService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<GameDto>> GetAllAsync()
        {
            var result = await _client.GetFromJsonAsync<IEnumerable<GameDto>>(ApiEndpoints.Games.GetAll);
            return result ?? [];
        }

        public async Task<GameDto> GetByIdAsync(Guid id)
        {
            var result = await _client.GetFromJsonAsync<GameDto>(string.Format(ApiEndpoints.Games.GetById, id));
            return result ?? new GameDto();
        }

        public async Task<GameDto> CreateAsync(CreateGameDto dto)
        {
            var response = await _client.PostAsJsonAsync(ApiEndpoints.Games.Base, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GameDto>() ?? new GameDto();
        }

        public async Task<GameDto> UpdateAsync(Guid id, UpdateGameDto dto)
        {
            var response = await _client.PutAsJsonAsync(string.Format(ApiEndpoints.Games.GetById, id), dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GameDto>() ?? new GameDto();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await _client.DeleteAsync(string.Format(ApiEndpoints.Games.GetById, id));
            response.EnsureSuccessStatusCode();
        }

        public async Task<UserDto> AcquireGameAsync(Guid userId, Guid gameId)
        {
            var response = await _client.PostAsync(
                string.Format(ApiEndpoints.Games.Acquire, gameId, userId), null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>() ?? new UserDto();
        }
    }
}
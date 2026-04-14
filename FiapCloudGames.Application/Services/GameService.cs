using FiapCloudGames.Application.DTOs;
using FiapCloudGames.Application.Interfaces;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Exceptions;
using FiapCloudGames.Domain.Interfaces;

namespace FiapCloudGames.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;

        public GameService(IGameRepository gameRepository, IUserRepository userRepository)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }

        public async Task<GameDto> GetByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id)
                ?? throw new DomainException($"Jogo com Id '{id}' não encontrado.");

            return MapToDto(game);
        }

        public async Task<IEnumerable<GameDto>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return games.Select(MapToDto);
        }

        public async Task<GameDto> CreateAsync(CreateGameDto dto)
        {
            var game = new Game(dto.Title, dto.Price, dto.Description);
            await _gameRepository.AddAsync(game);
            return MapToDto(game);
        }

        public async Task<GameDto> UpdateAsync(int id, UpdateGameDto dto)
        {
            var game = await _gameRepository.GetByIdAsync(id)
                ?? throw new DomainException($"Jogo com Id '{id}' não encontrado.");

            game.Update(dto.Title, dto.Price, dto.Description);
            await _gameRepository.UpdateAsync(game);

            return MapToDto(game);
        }

        public async Task DeleteAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id)
                ?? throw new DomainException($"Jogo com Id '{id}' não encontrado.");

            await _gameRepository.DeleteAsync(game.Id);
        }

        public async Task<UserDto> AcquireGameAsync(int userId, int gameId)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new DomainException($"Usuário com Id '{userId}' não encontrado.");

            var game = await _gameRepository.GetByIdAsync(gameId)
                ?? throw new DomainException($"Jogo com Id '{gameId}' não encontrado.");

            if (!game.IsActive)
                throw new DomainException("Não é possível adquirir um jogo inativo.");

            user.AddGame(game);
            await _userRepository.UpdateAsync(user);

            return new UserDto(user.Id, user.Name, user.Email.Value, user.Role, user.CreatedAt);
        }

        private static GameDto MapToDto(Game game) => new(
            game.Id,
            game.Title,
            game.Description,
            game.Price,
            game.IsActive,
            game.CreatedAt
        );
    }
}

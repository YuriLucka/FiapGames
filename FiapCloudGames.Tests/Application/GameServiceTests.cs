using FiapCloudGames.Application.DTOs;
using FiapCloudGames.Application.Services;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Exceptions;
using FiapCloudGames.Domain.Interfaces;
using Moq;

namespace FiapCloudGames.Tests.Application
{
    public class GameServiceTests
    {
        private readonly Mock<IGameRepository> _gameRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _gameRepoMock = new Mock<IGameRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _gameService = new GameService(_gameRepoMock.Object, _userRepoMock.Object);
        }

        // ── CreateAsync ─────────────────────────────────────────────────────────

        [Fact]
        public async Task CreateAsync_DeveCriarJogo_QuandoDadosValidos()
        {
            var dto = new CreateGameDto("Counter Strike", 59.90m, "FPS");
            _gameRepoMock.Setup(r => r.AddAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);

            var result = await _gameService.CreateAsync(dto);

            Assert.Equal("Counter Strike", result.Title);
            Assert.Equal(59.90m, result.Price);
            Assert.True(result.IsActive);

            _gameRepoMock.Verify(r => r.AddAsync(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_DeveLancarExcecao_QuandoTituloVazio()
        {
            var dto = new CreateGameDto("", 59.90m, null);

            await Assert.ThrowsAsync<DomainException>(() =>
                _gameService.CreateAsync(dto));

            _gameRepoMock.Verify(r => r.AddAsync(It.IsAny<Game>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_DeveLancarExcecao_QuandoPrecoNegativo()
        {
            var dto = new CreateGameDto("Jogo", -1m, null);

            await Assert.ThrowsAsync<DomainException>(() =>
                _gameService.CreateAsync(dto));
        }

        // ── GetById ─────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetByIdAsync_DeveRetornarJogo_QuandoExistir()
        {
            var game = new Game("CS2", 59.90m);
            _gameRepoMock.Setup(r => r.GetByIdAsync(game.Id)).ReturnsAsync(game);

            var result = await _gameService.GetByIdAsync(game.Id);

            Assert.Equal(game.Id, result.Id);
            Assert.Equal("CS2", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_DeveLancarExcecao_QuandoNaoExistir()
        {
            _gameRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Game?)null);

            await Assert.ThrowsAsync<DomainException>(() =>
                _gameService.GetByIdAsync(999));
        }

        // ── UpdateAsync ─────────────────────────────────────────────────────────

        [Fact]
        public async Task UpdateAsync_DeveAtualizar_QuandoJogoExistir()
        {
            var game = new Game("Titulo Antigo", 10m);
            var dto = new UpdateGameDto("Titulo Novo", 99m, "Desc");

            _gameRepoMock.Setup(r => r.GetByIdAsync(game.Id)).ReturnsAsync(game);
            _gameRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);

            var result = await _gameService.UpdateAsync(game.Id, dto);

            Assert.Equal("Titulo Novo", result.Title);
            Assert.Equal(99m, result.Price);
        }

        // ── AcquireGameAsync ────────────────────────────────────────────────────

        [Fact]
        public async Task AcquireGameAsync_DeveAdicionarJogoAoBiblioteca_QuandoJogoAtivo()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash");
            var game = new Game("CS2", 59.90m);

            _userRepoMock.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _gameRepoMock.Setup(r => r.GetByIdAsync(game.Id)).ReturnsAsync(game);
            _userRepoMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var result = await _gameService.AcquireGameAsync(user.Id, game.Id);

            Assert.Equal(user.Id, result.Id);
            Assert.Single(user.AcquiredGames);
        }

        [Fact]
        public async Task AcquireGameAsync_DeveLancarExcecao_QuandoJogoInativo()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash");
            var game = new Game("CS2", 59.90m);
            game.Deactivate();

            _userRepoMock.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _gameRepoMock.Setup(r => r.GetByIdAsync(game.Id)).ReturnsAsync(game);

            var ex = await Assert.ThrowsAsync<DomainException>(() =>
                _gameService.AcquireGameAsync(user.Id, game.Id));

            Assert.Equal("Não é possível adquirir um jogo inativo.", ex.Message);
        }

        [Fact]
        public async Task AcquireGameAsync_DeveLancarExcecao_QuandoJogoJaNaBiblioteca()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash");
            var game = new Game("CS2", 59.90m);

            user.AddGame(game); // já está na biblioteca

            _userRepoMock.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _gameRepoMock.Setup(r => r.GetByIdAsync(game.Id)).ReturnsAsync(game);

            await Assert.ThrowsAsync<DomainException>(() =>
                _gameService.AcquireGameAsync(user.Id, game.Id));
        }
    }
}

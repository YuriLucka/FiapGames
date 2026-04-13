using FiapCloudGames.API.Controllers;
using FiapCloudGames.Application.DTOs;
using FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace FiapCloudGames.Tests.API
{
    public class BibliotecaControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly BibliotecaController _controller;

        public BibliotecaControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new BibliotecaController(_userServiceMock.Object);
        }

        private void SetUsuarioAutenticado(int usuarioId)
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()) };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        // ── MeusJogos ───────────────────────────────────────────────────────────

        [Fact]
        public async Task MeusJogos_DeveRetornarListaPopulada_QuandoUsuarioTemJogos()
        {
            SetUsuarioAutenticado(1);

            var jogos = new List<MeusJogosViewModel>
            {
                new(1, "CS2", "FPS competitivo", 59.90m, DateTime.UtcNow),
                new(2, "Hades", "Roguelike", 39.90m, DateTime.UtcNow)
            };

            _userServiceMock.Setup(s => s.ObterJogosDoUsuarioAsync(1))
                .ReturnsAsync(jogos);

            var result = await _controller.MeusJogos();

            var ok = Assert.IsType<OkObjectResult>(result);
            var lista = Assert.IsAssignableFrom<IEnumerable<MeusJogosViewModel>>(ok.Value);
            Assert.Equal(2, lista.Count());
        }

        [Fact]
        public async Task MeusJogos_DeveRetornarListaVazia_QuandoUsuarioNaoTemJogos()
        {
            SetUsuarioAutenticado(1);

            _userServiceMock.Setup(s => s.ObterJogosDoUsuarioAsync(1))
                .ReturnsAsync(Enumerable.Empty<MeusJogosViewModel>());

            var result = await _controller.MeusJogos();

            var ok = Assert.IsType<OkObjectResult>(result);
            var lista = Assert.IsAssignableFrom<IEnumerable<MeusJogosViewModel>>(ok.Value);
            Assert.Empty(lista);
        }

        [Fact]
        public async Task MeusJogos_DeveRetornarUnauthorized_QuandoSemAutenticacao()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var result = await _controller.MeusJogos();

            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}

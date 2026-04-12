using FiapCloudGames.Application.DTOs;
using FiapCloudGames.Application.Services;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using FiapCloudGames.Domain.Exceptions;
using FiapCloudGames.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FiapCloudGames.Tests.Application
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepoMock.Object);
        }

        // ── GetById ─────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetByIdAsync_DeveRetornarUser_QuandoExistir()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash");
            _userRepoMock.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);

            var result = await _userService.GetByIdAsync(user.Id);

            Assert.Equal(user.Id, result.Id);
            Assert.Equal("yuri@fiap.com", result.Email);
        }

        [Fact]
        public async Task GetByIdAsync_DeveLancarExcecao_QuandoNaoExistir()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<DomainException>(() =>
                _userService.GetByIdAsync(999));
        }

        // ── CreateAsync ─────────────────────────────────────────────────────────

        [Fact]
        public async Task CreateAsync_DeveCriarUser_QuandoDadosValidos()
        {
            var dto = new CreateUserDto("Yuri", "yuri@fiap.com", "Senha@123");

            _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);
            _userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var result = await _userService.CreateAsync(dto);

            Assert.Equal("Yuri", result.Name);
            Assert.Equal("yuri@fiap.com", result.Email);
            Assert.Equal(UserRole.User, result.Role);

            _userRepoMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_DeveLancarExcecao_QuandoEmailJaExistir()
        {
            var dto = new CreateUserDto("Yuri", "yuri@fiap.com", "Senha@123");
            var existingUser = new User("Outro", "yuri@fiap.com", "hash");

            _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync(existingUser);

            var ex = await Assert.ThrowsAsync<DomainException>(() =>
                _userService.CreateAsync(dto));

            Assert.Equal("Já existe um usuário com este e-mail.", ex.Message);
            _userRepoMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
        }

        [Theory]
        [InlineData("semnum@fiap.com", "SenhaSemNum@")]
        [InlineData("semletra@fiap.com", "12345678@")]
        [InlineData("semespecial@fiap.com", "Senha1234")]
        [InlineData("curta@fiap.com", "Ab@1")]
        public async Task CreateAsync_DeveLancarExcecao_QuandoSenhaInvalida(string email, string senha)
        {
            var dto = new CreateUserDto("Yuri", email, senha);
            _userRepoMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<DomainException>(() =>
                _userService.CreateAsync(dto));

            _userRepoMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
        }

        // ── DeleteAsync ─────────────────────────────────────────────────────────

        [Fact]
        public async Task DeleteAsync_DeveDeletar_QuandoUserExistir()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash");
            _userRepoMock.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _userRepoMock.Setup(r => r.DeleteAsync(user.Id)).Returns(Task.CompletedTask);

            await _userService.DeleteAsync(user.Id);

            _userRepoMock.Verify(r => r.DeleteAsync(user.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveLancarExcecao_QuandoUserNaoExistir()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<DomainException>(() =>
                _userService.DeleteAsync(999));
        }

        // ── PromoteToAdmin ──────────────────────────────────────────────────────

        [Fact]
        public async Task PromoteToAdminAsync_DevePromover_QuandoUserExistir()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash");
            _userRepoMock.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _userRepoMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            await _userService.PromoteToAdminAsync(user.Id);

            Assert.Equal(UserRole.Admin, user.Role);
            _userRepoMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }
    }
}

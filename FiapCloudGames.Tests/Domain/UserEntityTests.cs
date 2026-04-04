using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using FiapCloudGames.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGames.Tests.Domain
{
    /// <summary>
    /// Testes da entidade User seguindo TDD.
    /// Cada teste valida uma regra de negócio do domínio.
    /// </summary>
    public class UserEntityTests
    {
        // ── Criação válida ──────────────────────────────────────────────────────

        [Fact]
        public void User_DeveSerCriado_QuandoDadosValidos()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash_valido");

            Assert.Equal("Yuri", user.Name);
            Assert.Equal("yuri@fiap.com", user.Email.Value);
            Assert.Equal(UserRole.User, user.Role);
            Assert.NotEqual(Guid.Empty, user.Id);
        }

        [Fact]
        public void User_DeveTerRoleAdmin_QuandoCriadoComoAdmin()
        {
            var user = new User("Admin", "admin@fiap.com", "hash_valido", UserRole.Admin);

            Assert.Equal(UserRole.Admin, user.Role);
        }

        // ── Validação de nome ───────────────────────────────────────────────────

        [Fact]
        public void User_DeveLancarExcecao_QuandoNomeVazio()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new User("", "yuri@fiap.com", "hash_valido"));

            Assert.Equal("Nome é obrigatório.", ex.Message);
        }

        [Fact]
        public void User_DeveLancarExcecao_QuandoNomeNulo()
        {
            Assert.Throws<DomainException>(() =>
                new User(null!, "yuri@fiap.com", "hash_valido"));
        }

        // ── Validação de e-mail ─────────────────────────────────────────────────

        [Theory]
        [InlineData("emailsemaroba")]
        [InlineData("email@")]
        [InlineData("@dominio.com")]
        [InlineData("")]
        public void User_DeveLancarExcecao_QuandoEmailInvalido(string emailInvalido)
        {
            Assert.Throws<DomainException>(() =>
                new User("Yuri", emailInvalido, "hash_valido"));
        }

        [Fact]
        public void User_DeveArmazenarEmail_EmMinusculo()
        {
            var user = new User("Yuri", "YURI@FIAP.COM", "hash_valido");

            Assert.Equal("yuri@fiap.com", user.Email.Value);
        }

        // ── Validação de senha (texto puro) ─────────────────────────────────────

        [Fact]
        public void ValidateRawPassword_DeveLancarExcecao_QuandoMenosDe8Caracteres()
        {
            var ex = Assert.Throws<DomainException>(() =>
                User.ValidateRawPassword("Ab@1"));

            Assert.Equal("Senha deve ter no mínimo 8 caracteres.", ex.Message);
        }

        [Fact]
        public void ValidateRawPassword_DeveLancarExcecao_QuandoSemNumeros()
        {
            var ex = Assert.Throws<DomainException>(() =>
                User.ValidateRawPassword("SenhaS@em"));

            Assert.Equal("Senha deve conter números.", ex.Message);
        }

        [Fact]
        public void ValidateRawPassword_DeveLancarExcecao_QuandoSemLetras()
        {
            var ex = Assert.Throws<DomainException>(() =>
                User.ValidateRawPassword("12345678@"));

            Assert.Equal("Senha deve conter letras.", ex.Message);
        }

        [Fact]
        public void ValidateRawPassword_DeveLancarExcecao_QuandoSemCaracterEspecial()
        {
            var ex = Assert.Throws<DomainException>(() =>
                User.ValidateRawPassword("Senha1234"));

            Assert.Equal("Senha deve conter caracteres especiais.", ex.Message);
        }

        [Fact]
        public void ValidateRawPassword_NaoDeveLancarExcecao_QuandoSenhaValida()
        {
            var exception = Record.Exception(() =>
                User.ValidateRawPassword("Senha@123"));

            Assert.Null(exception);
        }

        // ── Biblioteca de jogos ─────────────────────────────────────────────────

        [Fact]
        public void User_DeveAdicionarJogo_QuandoJogoNaoEstaNaBiblioteca()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash_valido");
            var game = new Game("Jogo Teste", 29.90m);

            user.AddGame(game);

            Assert.Single(user.AcquiredGames);
            Assert.Contains(game, user.AcquiredGames);
        }

        [Fact]
        public void User_DeveLancarExcecao_QuandoJogoJaEstaNaBiblioteca()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash_valido");
            var game = new Game("Jogo Teste", 29.90m);

            user.AddGame(game);

            var ex = Assert.Throws<DomainException>(() => user.AddGame(game));
            Assert.Equal("Jogo já está na biblioteca do usuário.", ex.Message);
        }

        // ── Promoção de role ────────────────────────────────────────────────────

        [Fact]
        public void User_DeveSerPromovido_ParaAdmin()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash_valido");

            user.PromoteToAdmin();

            Assert.Equal(UserRole.Admin, user.Role);
        }

        [Fact]
        public void User_DeveSerRebaixado_ParaUser()
        {
            var user = new User("Yuri", "yuri@fiap.com", "hash_valido", UserRole.Admin);

            user.DemoteToUser();

            Assert.Equal(UserRole.User, user.Role);
        }
    }
}

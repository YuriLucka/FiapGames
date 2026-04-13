using FiapCloudGames.Application.DTOs;
using FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FiapCloudGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BibliotecaController : ControllerBase
    {
        private readonly IUserService _userService;

        public BibliotecaController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retorna a lista de jogos da biblioteca do usuário autenticado.
        /// </summary>
        /// <response code="200">Lista de jogos retornada com sucesso (pode ser vazia).</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpGet("meus-jogos")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<MeusJogosViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> MeusJogos()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var usuarioId))
                return Unauthorized();

            var jogos = await _userService.ObterJogosDoUsuarioAsync(usuarioId);
            return Ok(jogos);
        }
    }
}

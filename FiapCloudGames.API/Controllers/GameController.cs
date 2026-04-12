using FiapCloudGames.Application.DTOs;
using FiapCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>Lista todos os jogos.</summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAllAsync();
            return Ok(games);
        }

        /// <summary>Retorna um jogo pelo Id.</summary>
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            return Ok(game);
        }

        /// <summary>Cria um novo jogo. Apenas Admin.</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateGameDto dto)
        {
            var game = await _gameService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
        }

        /// <summary>Atualiza um jogo. Apenas Admin.</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGameDto dto)
        {
            var game = await _gameService.UpdateAsync(id, dto);
            return Ok(game);
        }

        /// <summary>Remove um jogo. Apenas Admin.</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _gameService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>Usuário adquire um jogo para sua biblioteca.</summary>
        [HttpPost("{gameId:int}/acquire/{userId:int}")]
        [Authorize]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Acquire(int gameId, int userId)
        {
            var user = await _gameService.AcquireGameAsync(userId, gameId);
            return Ok(user);
        }
    }
}

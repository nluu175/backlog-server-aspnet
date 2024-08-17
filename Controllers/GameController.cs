using Microsoft.AspNetCore.Mvc;
using BacklogAPI.Dtos;
using BacklogAPI.Repository;
using BacklogAPI.Mappers;

namespace BacklogAPI.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly GameRepository _gameRepository;

        public GameController(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // GET: /api/games
        [HttpGet]
        public IActionResult GetGames()
        {
            var games = _gameRepository.GetGames();
            var gamesDto = games.Select(g => g.ToGameDto()).ToList();

            return Ok(gamesDto);
        }

        // GET: /api/games/{game_id}
        [HttpGet("{game_id}")]
        public IActionResult GetGame(Guid game_id)
        {
            var game = _gameRepository.GetGame(game_id);
            if (game == null)
            {
                return NotFound();
            }
            var gameDto = game.ToGameDto();

            return Ok(gameDto);
        }

        // POST: /api/games
        [HttpPost]
        public IActionResult CreateGame([FromBody] CreateGameDto gameDto)
        {
            // NOTE: Check out "this"
            var game = gameDto.ToGameFromCreateGameDto(_gameRepository);
            var createdGame = _gameRepository.CreateGame(game);
            var createdGameDto = createdGame.ToGameDto();

            return CreatedAtAction(nameof(GetGame), new { game_id = createdGameDto.Id }, createdGameDto);
        }
    }
}
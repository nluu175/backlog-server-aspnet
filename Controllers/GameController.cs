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
        private readonly GenreRepository _genreRepository;

        public GameController(GameRepository gameRepository, GenreRepository genreRepository)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
        }

        // GET: /api/games
        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var games = await _gameRepository.GetGames();
            var gamesDto = games.Select(g => g.ToGameDto()).ToList();

            return Ok(gamesDto);
        }

        // GET: /api/games/{game_id}
        [HttpGet("{game_id}")]
        public async Task<IActionResult> GetGame(Guid game_id)
        {
            var game = await _gameRepository.GetGame(game_id);
            if (game == null)
            {
                return NotFound();
            }
            var gameDto = game.ToGameDto();

            return Ok(gameDto);
        }

        // POST: /api/games
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameDto gameDto)
        {
            // NOTE: Check out "this"
            var game = gameDto.ToGameFromCreateGameDto(_genreRepository);
            var createdGame = await _gameRepository.CreateGame(game);
            var createdGameDto = createdGame.ToGameDto();

            return CreatedAtAction(nameof(CreateGame), new { game_id = createdGameDto.Id }, createdGameDto);
        }
    }
}
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

        // GET: /backlog/games
        [HttpGet]
        public IActionResult GetGames()
        {
            var games = _gameRepository.GetGames();
            var gamesDto = games.Select(g => g.ToGameDto()).ToList();

            return Ok(gamesDto);
        }

        // GET: /backlog/games/{game_id}
        [HttpGet("{game_id}")]
        public IActionResult GetGame(Guid game_id)
        {
            var gameDto = _gameRepository.GetGame(game_id);
            if (gameDto == null)
            {
                return NotFound();
            }

            // TODO: https://github.com/teddysmithdev/FinShark/blob/master/api/Controllers/StockController.cs
            return Ok(gameDto);
        }

        // POST: /backlog/games
        [HttpPost]
        public IActionResult CreateGame([FromBody] CreateGameDto gameDto)
        {
            var createGameDto = _gameRepository.CreateGame(gameDto);
            return Ok(createGameDto);
        }
    }
}
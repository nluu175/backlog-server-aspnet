using Microsoft.AspNetCore.Mvc;
using BacklogAPI.Models;
using BacklogAPI.Data;
using BacklogAPI.DTOs;
using AutoMapper;

namespace BacklogAPI.Controllers
{
    [ApiController]
    [Route("backlog/games")]
    public class GameController : ControllerBase
    {
        private readonly BacklogDbContext _context;
        private readonly IMapper _mapper;

        public GameController(BacklogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: /backlog/games/{game_id}
        [HttpGet("{game_id}")]
        public IActionResult GetGame(Guid game_id)
        {
            var game = _context.Games.Find(game_id);
            if (game == null)
            {
                return NotFound();
            }

            var gameDto = _mapper.Map<GameDto>(game);
            return Ok(gameDto);
        }

        // GET: /backlog/games
        [HttpGet]
        public IActionResult GetGames([FromQuery] int? page, [FromQuery] int? size)
        {
            var games = _context.Games.AsQueryable();

            if (page.HasValue && size.HasValue)
            {
                var paginatedGames = games.Skip((page.Value - 1) * size.Value).Take(size.Value).ToList();
                var paginatedGameDtos = _mapper.Map<List<GameDto>>(paginatedGames);
                return Ok(paginatedGameDtos);
            }

            var gameDtos = _mapper.Map<List<GameDto>>(games.ToList());
            return Ok(gameDtos);
        }

        // POST: /backlog/games
        [HttpPost]
        public IActionResult CreateGame([FromBody] GameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = _mapper.Map<Game>(gameDto);
            _context.Games.Add(game);
            _context.SaveChanges();

            var createdGameDto = _mapper.Map<GameDto>(game);
            return CreatedAtAction(nameof(GetGame), new { game_id = game.Id }, createdGameDto);
        }
    }
}
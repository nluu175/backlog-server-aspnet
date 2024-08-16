using BacklogAPI.Data;
using BacklogAPI.Dtos;
using BacklogAPI.Mappers;
using BacklogAPI.Models;

namespace BacklogAPI.Repository
{
    public class GameRepository
    {
        private readonly BacklogDbContext _context;

        public GameRepository(BacklogDbContext context)
        {
            _context = context;
        }

        public ICollection<Game> GetGames()
        {
            var games = _context.Games.ToList();
            return games;
        }

        public GameDto? GetGame(Guid gameId)
        {
            var game = _context.Games.Find(gameId);
            return game?.ToGameDto();
        }

        public GameDto CreateGame(CreateGameDto gameDto)
        {
            var game = gameDto.ToGameFromCreateGameDto(this);
            _context.Games.Add(game);
            _context.SaveChanges();
            return game.ToGameDto();
        }

        public Genre? GetGenreById(Guid id)
        {
            return _context.Genres.FirstOrDefault(g => g.Id == id);
        }
    }
}
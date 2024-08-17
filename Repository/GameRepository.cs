using BacklogAPI.Data;
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

        public Game? GetGame(Guid gameId)
        {
            var game = _context.Games.Find(gameId);
            return game;
        }

        public Game CreateGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }
    }
}
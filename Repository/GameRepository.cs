using BacklogAPI.Data;
using BacklogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BacklogAPI.Repository
{
    public class GameRepository
    {
        private readonly BacklogDbContext _context;

        public GameRepository(BacklogDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Game>> GetGames()
        {
            var games = await _context.Games.ToListAsync();
            return games;
        }

        public async Task<Game?> GetGame(Guid gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            return game;
        }

        public async Task<Game> CreateGame(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return game;
        }
    }
}
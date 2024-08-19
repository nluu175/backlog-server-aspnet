using BacklogAPI.Data;
using BacklogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BacklogAPI.Repository
{
    public class UpdateRepository
    {
        private readonly BacklogDbContext _context;

        public UpdateRepository(BacklogDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserBySteamIdAsync(string steamId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.SteamId == steamId);
        }
    }
}
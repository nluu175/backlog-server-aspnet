using Microsoft.EntityFrameworkCore; // Add this using statement
using BacklogAPI.Models;

namespace BacklogAPI.Data
{
    public class BacklogDbContext : DbContext
    {
        public BacklogDbContext(DbContextOptions<BacklogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}


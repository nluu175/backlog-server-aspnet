using BacklogAPI.Data;
using BacklogAPI.Models;

namespace BacklogAPI.Repository
{
    public class GenreRepository
    {
        private readonly BacklogDbContext _context;

        public GenreRepository(BacklogDbContext context)
        {
            _context = context;
        }

        public ICollection<Genre> GetGenres()
        {
            var genres = _context.Genres.ToList();
            return genres;
        }

        public Genre? GetGenre(Guid genreId)
        {
            var genre = _context.Genres.Find(genreId);
            return genre;
        }

        public Genre CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre? GetGenreById(Guid id)
        {
            return _context.Genres.FirstOrDefault(g => g.Id == id);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using BacklogAPI.Dtos;
using BacklogAPI.Repository;
using BacklogAPI.Mappers;

namespace BacklogAPI.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly GenreRepository _genreRepository;

        public GenreController(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        // GET: /api/genres
        [HttpGet]
        public IActionResult GetGenres()
        {
            var genres = _genreRepository.GetGenres();
            var genresDto = genres.Select(g => g.ToGenreDto()).ToList();

            return Ok(genresDto);
        }

        // GET: /api/genres/{genre_id}
        [HttpGet("{genre_id}")]
        public IActionResult GetGenre(Guid genre_id)
        {
            var genre = _genreRepository.GetGenre(genre_id);
            if (genre == null)
            {
                return NotFound();
            }
            var genreDto = genre.ToGenreDto();

            return Ok(genreDto);
        }
    }

}
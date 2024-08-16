using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BacklogAPI.Data;
using BacklogAPI.Dtos;
using BacklogAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BacklogAPI.Controllers
{
    [ApiController]
    [Route("backlog/genres")]
    public class GenreController : ControllerBase
    {
        private readonly BacklogDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BacklogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: /backlog/genres/{genre_id}
        [HttpGet("{genre_id}")]
        public async Task<IActionResult> GetGenre(Guid genre_id)
        {
            var genre = await _context.Genres.FindAsync(genre_id);

            if (genre == null)
            {
                return NotFound();
            }

            var genreDto = _mapper.Map<GenreDto>(genre);
            return Ok(genreDto);
        }

        // GET: /backlog/genres
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _context.Genres.ToListAsync();
            var genreDtos = _mapper.Map<List<GenreDto>>(genres);
            return Ok(genreDtos);
        }
    }
}
using BacklogAPI.Dtos;
using BacklogAPI.Models;
using BacklogAPI.Repository;

namespace BacklogAPI.Mappers
{
    public static class GenreMappers
    {
        // -- Genre
        public static GenreDto ToGenreDto(this Genre genreModel)
        {
            return new GenreDto
            {
                Id = genreModel.Id,
                Name = genreModel.Name,
                Code = genreModel.Code
            };
        }

        public static Genre ToGenreFromCreateGenreDto(this CreateGenreDto genreDto)
        {
            return new Genre
            {
                Name = genreDto.Name ?? string.Empty,
                Code = genreDto.Code ?? string.Empty
            };
        }
    }
}
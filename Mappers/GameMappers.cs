using BacklogAPI.Dtos;
using BacklogAPI.Models;
using BacklogAPI.Repository;

namespace BacklogAPI.Mappers
{
    public static class GameMappers
    {
        // -- Game
        public static GameDto ToGameDto(this Game gameModel)
        {
            return new GameDto
            {
                Id = gameModel.Id,
                Name = gameModel.Name,
                Description = gameModel.Description,
                ReleaseDate = gameModel.ReleaseDate,
                SteamAppId = gameModel.SteamAppId,
                Genres = gameModel.Genres?.Select(g => g.Id).ToList() ?? new List<Guid>(),
                Platform = gameModel.Platform
            };
        }

        public static Game ToGameFromCreateGameDto(this CreateGameDto gameDto, GameRepository gameRepository)
        {
            return new Game
            {
                Name = gameDto.Name,
                Description = gameDto.Description,
                ReleaseDate = gameDto.ReleaseDate,
                SteamAppId = gameDto.SteamAppId,
                Genres = gameDto.Genres
                    .Select(genreId => gameRepository.GetGenreById(genreId))
                    .Where(g => g != null)
                    .ToList()!,
                Platform = gameDto.Platform
            };
        }

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
    }
}
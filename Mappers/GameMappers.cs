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
                Genres = gameModel.Genres?.Select(g => g.Id).ToList() ?? [],
                Platform = gameModel.Platform
            };
        }

        public static Game ToGameFromCreateGameDto(this CreateGameDto gameDto, GenreRepository genreRepository)
        {
            return new Game
            {
                Name = gameDto.Name,
                Description = gameDto.Description,
                ReleaseDate = gameDto.ReleaseDate,
                SteamAppId = gameDto.SteamAppId,
                Genres = gameDto.Genres
                    .Select(genreId => genreRepository.GetGenreById(genreId))
                    .Where(g => g != null)
                    .ToList()!,
                Platform = gameDto.Platform
            };
        }
    }
}
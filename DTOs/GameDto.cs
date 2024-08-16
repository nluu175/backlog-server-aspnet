using BacklogAPI.Models;

namespace BacklogAPI.Dtos
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int SteamAppId { get; set; }
        public required List<Guid> Genres { get; set; }
        public Game.PlatformTypes Platform { get; set; }
    }

    public class CreateGameDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int SteamAppId { get; set; }
        public required List<Guid> Genres { get; set; }
        public Game.PlatformTypes Platform { get; set; }
    }
}
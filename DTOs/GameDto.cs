namespace BacklogAPI.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SteamAppId { get; set; }
        public List<GenreDto>? Genres { get; set; }
        public string? Platform { get; set; }
    }
}
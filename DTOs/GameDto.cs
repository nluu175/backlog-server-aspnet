namespace BacklogAPI.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public string Platform { get; set; }
        public int SteamAppId { get; set; }
    }
}
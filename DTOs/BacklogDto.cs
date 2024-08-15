namespace BacklogAPI.DTOs
{
    public class BacklogDto
    {
        public Guid Id { get; set; }
        public UserDto? User { get; set; }
        public GameDto? Game { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }
        public string? Name { get; set; }
        public int SteamAppId { get; set; }
    }
}
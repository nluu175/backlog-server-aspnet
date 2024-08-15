namespace BacklogAPI.DTOs
{
    public class BacklogDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public GameDto Game { get; set; }
        public string Status { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int Playtime { get; set; }
        public string Name => Game?.Name;
        public int SteamAppId => Game?.SteamAppId ?? 0;
    }
}
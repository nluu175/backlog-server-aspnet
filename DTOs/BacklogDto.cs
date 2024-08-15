namespace BacklogAPI.DTOs
{
    public class BacklogDto
    {
        public enum StatusTypes
        {
            NOT_STARTED = 0,
            IN_PROGRESS = 1,
            COMPLETED = 2
        }
        public Guid Id { get; set; }
        public UserDto? User { get; set; }
        public GameDto? Game { get; set; }
        public StatusTypes Status { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public float Playtime { get; set; }
        public string Name { get; set; }
        public int SteamAppId { get; set; }
    }
}
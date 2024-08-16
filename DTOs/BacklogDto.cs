namespace BacklogAPI.Dtos
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
        public StatusTypes Status { get; set; }
        public float Rating { get; set; }
        public required string Comment { get; set; }
        public required float Playtime { get; set; }
        public required string Name { get; set; }
        public int SteamAppId { get; set; }
    }
}
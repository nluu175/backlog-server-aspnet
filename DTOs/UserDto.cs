namespace BacklogAPI.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string SteamId { get; set; }
    }
}
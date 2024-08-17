namespace BacklogAPI.Dtos
{
    public class GenreDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }

    public class CreateGenreDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
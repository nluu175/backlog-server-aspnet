using System.ComponentModel.DataAnnotations;

namespace BacklogAPI.Models
{
    public class Genre
    {
        public override string ToString()
        {
            return $"{Name} - ({Id})";
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Code { get; set; } = string.Empty;
    }
}
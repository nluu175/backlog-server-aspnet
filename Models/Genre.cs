using System;
using System.ComponentModel.DataAnnotations;

namespace BacklogAPI.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Code must be lower case and spaces replaced with hyphens.")]
        public string Code { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name} - ({Id})";
        }
    }
}
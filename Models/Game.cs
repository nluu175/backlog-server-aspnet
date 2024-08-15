using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BacklogAPI.Models
{
    public class Game
    {
        public enum PlatformTypes
        {
            Steam = 0,
            Switch = 1
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public required ICollection<Genre> Genres { get; set; }
        public PlatformTypes? Platform { get; set; }

        [Required]
        public int SteamAppId { get; set; }

        public bool WasReleasedRecently()
        {
            return ReleaseDate >= DateTime.Now.AddDays(-1);
        }
    }
}
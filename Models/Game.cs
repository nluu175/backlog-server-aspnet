using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BacklogAPI.Models
{
    public class Game
    {
        public enum PlatformTypes
        {
            STEAM = 0,
            SWITCH = 1
        }

        public bool WasReleasedRecently()
        {
            return ReleaseDate >= DateTime.Now.AddDays(-1);
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required DateTime ReleaseDate { get; set; }

        [Required]
        public required ICollection<Genre> Genres { get; set; }

        [Required]
        public required PlatformTypes Platform { get; set; }

        [Required]
        public required int SteamAppId { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace BacklogAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [MaxLength(100)]
        public required string SteamId { get; set; }

        // One to One object with .NET default user
        // public User User { get; set; }
    }
}
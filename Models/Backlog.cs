using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BacklogAPI.Models
{

    public class Backlog
    {
        public enum StatusTypes
        {
            NOT_STARTED = 0,
            IN_PROGRESS = 1,
            COMPLETED = 2
        }

        public override string ToString()
        {
            return $"{User.SteamId} - {Game.Name} - ({Id})";
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public int UserId { get; set; }
        public required User User { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public required Game Game { get; set; }

        public StatusTypes? Status { get; set; }

        public double? Rating { get; set; }

        [MaxLength(1000)]
        public required string Comment { get; set; }

        public int Playtime { get; set; }

        public int UniqueUserGameIndex { get; set; }
    }
}
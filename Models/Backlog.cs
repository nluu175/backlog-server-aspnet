using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BacklogAPI.Models
{

    public class Backlog

    // NOTE: Navigation properties are not included in the SQL schema

    // --- SQL Schema
    // CREATE TABLE "Backlogs" (
    //     "Id" TEXT NOT NULL PRIMARY KEY,
    //     "UserId" TEXT NOT NULL,
    //     "GameId" TEXT NOT NULL,
    //     "Status" INTEGER NOT NULL,
    //     "Rating" REAL NOT NULL,
    //     "Comment" TEXT NOT NULL,
    //     "Playtime" INTEGER NOT NULL,
    //     FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE,
    //     FOREIGN KEY ("GameId") REFERENCES "Games" ("Id") ON DELETE CASCADE
    // );

    // The [Required] attribute is used for runtime validation, often in scenarios involving nullable or reference types 
    // The required keyword ensures that a property is set at compile-time, providing stricter initialization guarantees.


    {
        public enum StatusTypes
        {
            NOT_STARTED = 0,
            IN_PROGRESS = 1,
            COMPLETED = 2
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public required User User { get; set; }

        [ForeignKey("Game")]
        public Guid GameId { get; set; }

        [Required]
        public required Game Game { get; set; }

        [Required]
        public required StatusTypes Status { get; set; }

        [Required]
        public required double Rating { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Comment { get; set; }

        [Required]
        public required int Playtime { get; set; }
    }
}
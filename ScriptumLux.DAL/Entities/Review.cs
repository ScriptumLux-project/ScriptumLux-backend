using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.DAL.Entities;

public class Review
{
    [Key]
    public int ReviewId { get; set; }

    public double Rating { get; set; }

    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    // FKs
    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
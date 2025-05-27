using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.DAL.Entities;

public class Review
{
    [Key]
    public int ReviewId { get; set; }

    [Range(1, 10, ErrorMessage = "Рейтинг должен быть от 1 до 10")]
    public int Rating { get; set; }

    public string? Content { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // FKs
    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.DAL.Entities;

public class Comment
{
    [Key]
    public int CommentId { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    // FKs
    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
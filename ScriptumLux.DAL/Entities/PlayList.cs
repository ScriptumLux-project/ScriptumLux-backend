using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.DAL.Entities;

public class Playlist
{
    [Key]
    public int PlaylistId { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    // FK
    public int UserId { get; set; }
    public User User { get; set; }

    // Navigation
    public ICollection<PlaylistMovie> PlaylistMovies { get; set; }
}
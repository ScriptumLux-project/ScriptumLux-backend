using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.DAL.Entities;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string PasswordSalt { get; set; }

    [Required]
    public string Role { get; set; }

    public bool IsBanned { get; set; }

    // Navigation
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    public ICollection<History> HistoryRecords { get; set; } = new List<History>();
    public ICollection<Timecode> Timecodes { get; set; } = new List<Timecode>();
}
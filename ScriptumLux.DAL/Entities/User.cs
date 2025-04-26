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
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }

    public bool IsBanned { get; set; }

    // Navigation
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Playlist> Playlists { get; set; }
    public ICollection<History> HistoryRecords { get; set; }
    public ICollection<Timecode> Timecodes { get; set; }
}
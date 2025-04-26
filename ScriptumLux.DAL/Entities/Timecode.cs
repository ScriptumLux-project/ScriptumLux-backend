using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.DAL.Entities;

public class Timecode
{
    [Key]
    public int TimecodeId { get; set; }

    [Required]
    public string Label { get; set; }

    public TimeSpan Timestamp { get; set; }

    // FKs
    public int UserId { get; set; }
    public User User { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}
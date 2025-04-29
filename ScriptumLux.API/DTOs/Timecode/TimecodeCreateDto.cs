using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Timecode;

public class TimecodeCreateDto
{
    [Required]
    public string Label { get; set; }

    [Required]
    public TimeSpan Timestamp { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int MovieId { get; set; }
}
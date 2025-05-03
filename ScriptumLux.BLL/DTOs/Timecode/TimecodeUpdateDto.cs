using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Timecode;

public class TimecodeUpdateDto
{
    [Required]
    public int TimecodeId { get; set; }

    public string Label { get; set; }

    public TimeSpan? Timestamp { get; set; }
}
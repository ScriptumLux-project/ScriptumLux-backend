using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Timecode;

public class TimecodeCreateDto
{
    public string Label { get; set; } = null!;
    public string Timestamp { get; set; } = null!;
    public int UserId { get; set; }
    public int MovieId { get; set; }
}

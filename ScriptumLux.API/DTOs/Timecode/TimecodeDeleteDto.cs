using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Timecode;

public class TimecodeDeleteDto
{
    [Required]
    public int TimecodeId { get; set; }
}
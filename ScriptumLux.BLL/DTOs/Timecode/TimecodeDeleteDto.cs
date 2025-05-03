using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Timecode;

public class TimecodeDeleteDto
{
    [Required]
    public int TimecodeId { get; set; }
}
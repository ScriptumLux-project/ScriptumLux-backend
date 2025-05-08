using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Timecode;

public class TimecodeUpdateDto
{
    [Required]
    public int TimecodeId { get; set; }

    public string Label { get; set; }

    // Теперь строка, парсим вручную в сервисе
    public string Timestamp { get; set; }
}
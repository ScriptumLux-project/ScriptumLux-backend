using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.History;

public class HistoryDeleteDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int MovieId { get; set; }
}
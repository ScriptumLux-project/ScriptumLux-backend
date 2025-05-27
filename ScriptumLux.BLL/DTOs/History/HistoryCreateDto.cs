using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.History;

public class HistoryCreateDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required]
    public DateTime ViewedAt { get; set; }
}
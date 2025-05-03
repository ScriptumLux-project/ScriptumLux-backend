using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.History;

public class HistoryCreateDto
{
    [Required]
    public int MovieId { get; set; }

    [Required]
    public DateTime ViewedAt { get; set; }
}
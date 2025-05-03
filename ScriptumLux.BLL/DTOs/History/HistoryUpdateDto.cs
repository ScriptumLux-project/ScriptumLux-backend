using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.History;

public class HistoryUpdateDto
{
    [Required]
    public int MovieId { get; set; }

    public DateTime? ViewedAt { get; set; }  // Можно обновить дату
}
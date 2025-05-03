namespace ScriptumLux.BLL.DTOs.History;

public class HistoryDto
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public DateTime ViewedAt { get; set; }
}
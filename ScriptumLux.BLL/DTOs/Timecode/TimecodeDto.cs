namespace ScriptumLux.BLL.DTOs.Timecode;

public class TimecodeDto
{
    public int TimecodeId { get; set; }
    public string Label { get; set; }
    public TimeSpan Timestamp { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
}
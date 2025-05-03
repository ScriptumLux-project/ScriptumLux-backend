namespace ScriptumLux.BLL.DTOs.Playlist;

public class PlaylistDto
{
    public int PlaylistId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}
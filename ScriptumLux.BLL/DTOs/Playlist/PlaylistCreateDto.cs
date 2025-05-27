using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Playlist;

public class PlaylistCreateDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int UserId { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.PlaylistMovie;

public class PlaylistMovieUpdateDto
{
    [Required]
    public int PlaylistId { get; set; }

    [Required]
    public int MovieId { get; set; }

    public int? NewOrder { get; set; } 

}
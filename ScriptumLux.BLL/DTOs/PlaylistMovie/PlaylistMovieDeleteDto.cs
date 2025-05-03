using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.PlaylistMovie;

public class PlaylistMovieDeleteDto
{
    [Required]
    public int PlaylistId { get; set; }

    [Required]
    public int MovieId { get; set; }
}
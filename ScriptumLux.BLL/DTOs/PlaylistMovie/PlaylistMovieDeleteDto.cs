using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.PlaylistMovie;

public class PlaylistMovieDeleteDto
{
    [Required]
    public int PlaylistId { get; set; }

    [Required]
    public int MovieId { get; set; }
}
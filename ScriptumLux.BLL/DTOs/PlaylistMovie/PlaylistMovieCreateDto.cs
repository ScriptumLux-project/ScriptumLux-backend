using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.PlaylistMovie;

public class PlaylistMovieCreateDto
{
    [Required]
    public int PlaylistId { get; set; }

    [Required]
    public int MovieId { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.PlaylistMovie;

public class PlaylistMovieCreateDto
{
    [Required]
    public int PlaylistId { get; set; }

    [Required]
    public int MovieId { get; set; }
}
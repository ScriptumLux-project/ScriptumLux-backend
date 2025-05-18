using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Movie;

public class MovieCreateDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public int ReleaseYear { get; set; }

    //[Required]
   // public double Rating { get; set; }

    [Required]
    public string GenreName { get; set; }

    public string? PosterUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Description { get; set; }
}
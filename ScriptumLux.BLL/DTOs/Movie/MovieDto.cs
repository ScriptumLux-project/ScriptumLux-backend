namespace ScriptumLux.BLL.DTOs.Movie;

public class MovieDto
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
    public int GenreId { get; set; }

    // Новые свойства
    public string? PosterUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Description { get; set; }
}
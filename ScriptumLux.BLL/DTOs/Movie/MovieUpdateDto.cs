namespace ScriptumLux.BLL.DTOs.Movie;

public class MovieUpdateDto
{
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
    public string GenreName { get; set; }

    public string? PosterUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Description { get; set; }
}
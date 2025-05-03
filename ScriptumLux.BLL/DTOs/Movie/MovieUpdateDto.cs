namespace ScriptumLux.BLL.DTOs.Movie;

public class MovieUpdateDto
{
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
    public int GenreId { get; set; }
}
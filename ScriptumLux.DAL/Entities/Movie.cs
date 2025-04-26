using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.DAL.Entities;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    [Required]
    public string? Title { get; set; }

    public int ReleaseYear { get; set; }
    public double Rating { get; set; }

    // FK
    public int GenreId { get; set; }
    public Genre Genre { get; set; }

    // Navigation
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<PlaylistMovie> PlaylistMovies { get; set; }
    public ICollection<History> HistoryRecords { get; set; }
    public ICollection<Timecode> Timecodes { get; set; }
}
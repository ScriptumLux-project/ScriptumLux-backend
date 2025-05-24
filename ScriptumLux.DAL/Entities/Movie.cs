using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScriptumLux.DAL.Entities;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    [Required]
    public string Title { get; set; }
    
    public string? Country { get; set; }

    public int ReleaseYear { get; set; }
    
    // УДАЛЯЕМ поле Rating - теперь оно будет вычисляемым
    // public double Rating { get; set; } // УБИРАЕМ ЭТО ПОЛЕ!
    
    // Вместо этого добавляем поля для системы рейтинга
    [Column(TypeName = "decimal(3,2)")]
    public decimal AverageRating { get; set; } = 0; // Средний рейтинг (например, 7.85)
    
    public int TotalRatings { get; set; } = 0; // Общее количество оценок
    
    public int TotalRatingSum { get; set; } = 0; // Сумма всех оценок для быстрого пересчета

    public string? PosterUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Description { get; set; }

    // FK
    public int GenreId { get; set; }
    public Genre Genre { get; set; }

    // Navigation Properties
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<PlaylistMovie> PlaylistMovies { get; set; } = new List<PlaylistMovie>();
    public ICollection<History> HistoryRecords { get; set; } = new List<History>();
    public ICollection<Timecode> Timecodes { get; set; } = new List<Timecode>();
}
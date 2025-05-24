using ScriptumLux.BLL.DTOs.Comment;

namespace ScriptumLux.BLL.DTOs.Movie
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; } // Это поле будет заполняться из AverageRating
        public string Country { get; set; }
        public int GenreId { get; set; }
        
        // Добавляем дополнительные поля для полной информации о рейтинге
        public decimal AverageRating { get; set; }
        public int TotalRatings { get; set; }

        public string? PosterUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? Description { get; set; }

        // Добавляем комментарии
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
namespace ScriptumLux.BLL.DTOs.Movie;

public class MovieRatingDto
{
    public int MovieId { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalRatings { get; set; }
    public Dictionary<int, int> RatingDistribution { get; set; } = new(); // Распределение оценок (рейтинг -> количество)
}
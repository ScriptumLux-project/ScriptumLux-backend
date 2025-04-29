namespace ScriptumLux.API.DTOs.Review;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public double Rating { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
}
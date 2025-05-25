namespace ScriptumLux.BLL.DTOs.Review;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public int Rating { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; } 
    public int UserId { get; set; }
    public string UserName { get; set; } 
}
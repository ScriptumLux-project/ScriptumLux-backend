using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Review;

public class ReviewCreateDto
{
    [Range(1, 10, ErrorMessage = "Рейтинг должен быть от 1 до 10")]
    public int Rating { get; set; }
    
    [MaxLength(2000, ErrorMessage = "Максимальная длина отзыва 2000 символов")]
    public string? Content { get; set; }
    
    public int MovieId { get; set; }
    public int UserId { get; set; }
}
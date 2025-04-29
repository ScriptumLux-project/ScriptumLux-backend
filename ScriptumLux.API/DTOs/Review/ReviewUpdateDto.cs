using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Review;

public class ReviewUpdateDto
{
    [Required]
    public int ReviewId { get; set; }

    public double? Rating { get; set; }  // Возможно, не все обзоры будут обновляться по рейтингу
    public string Content { get; set; }
}
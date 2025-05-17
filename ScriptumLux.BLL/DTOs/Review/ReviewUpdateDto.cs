using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Review;

public class ReviewUpdateDto
{
    [Required]
    public int ReviewId { get; set; }

    public double? Rating { get; set; }
    public string Content { get; set; }
}
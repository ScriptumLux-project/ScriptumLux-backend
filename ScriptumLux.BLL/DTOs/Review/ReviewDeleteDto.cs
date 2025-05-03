using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Review;

public class ReviewDeleteDto
{
    [Required]
    public int ReviewId { get; set; }
}
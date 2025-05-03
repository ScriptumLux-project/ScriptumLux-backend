using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Review;

public class ReviewDeleteDto
{
    [Required]
    public int ReviewId { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Review;

public class ReviewCreateDto
{
    [Required]
    public double Rating { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required]
    public int UserId { get; set; }
}
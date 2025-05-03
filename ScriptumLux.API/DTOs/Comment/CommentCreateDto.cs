using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Comment;

public class CommentCreateDto
{
    [Required]
    public string Content { get; set; }

    [Required]
    public int MovieId { get; set; }
}
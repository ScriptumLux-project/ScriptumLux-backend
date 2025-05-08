using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Comment;

public class CommentCreateDto
{
    [Required]
    public string Content { get; set; }

    [Required]
    public int MovieId { get; set; }
    
    [Required]
    public int UserId { get; set; }
}
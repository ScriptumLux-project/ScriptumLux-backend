using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.API.DTOs.Comment;

public class CommentUpdateDto
{
    [Required]
    public string Content { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ScriptumLux.BLL.DTOs.Comment;

public class CommentUpdateDto
{
    [Required]
    public string Content { get; set; }
}
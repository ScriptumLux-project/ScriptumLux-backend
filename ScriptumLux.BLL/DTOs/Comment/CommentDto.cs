namespace ScriptumLux.API.DTOs.Comment;

public class CommentDto
{
    public int CommentId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
}
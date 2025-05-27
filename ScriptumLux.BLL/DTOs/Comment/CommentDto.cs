namespace ScriptumLux.BLL.DTOs.Comment
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
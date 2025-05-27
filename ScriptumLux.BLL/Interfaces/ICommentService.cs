using ScriptumLux.BLL.DTOs.Comment;

namespace ScriptumLux.BLL.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync();
        Task<CommentDto?> GetByIdAsync(int id);
        Task<IEnumerable<CommentDto>> GetByMovieIdAsync(int movieId);
        Task<CommentDto> CreateAsync(CommentCreateDto dto, int userId);
        Task<CommentDto?> UpdateAsync(int id, CommentUpdateDto dto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
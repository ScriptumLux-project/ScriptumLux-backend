using ScriptumLux.BLL.DTOs.Comment;

namespace ScriptumLux.BLL.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetAllAsync();
    Task<CommentDto?> GetByIdAsync(int id);
    Task<CommentDto> CreateAsync(CommentCreateDto dto, int userId);
    Task<CommentDto?> UpdateAsync(int id, CommentUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

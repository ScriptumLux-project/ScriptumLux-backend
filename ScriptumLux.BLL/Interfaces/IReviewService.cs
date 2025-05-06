using ScriptumLux.BLL.DTOs.Review;

namespace ScriptumLux.BLL.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<ReviewDto?> GetByIdAsync(int id);
    Task<ReviewDto> CreateAsync(ReviewCreateDto dto);
    Task<ReviewDto?> UpdateAsync(int id, ReviewUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
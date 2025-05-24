using ScriptumLux.BLL.DTOs.Movie;
using ScriptumLux.BLL.DTOs.Review;

namespace ScriptumLux.BLL.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<ReviewDto?> GetByIdAsync(int id);
    Task<IEnumerable<ReviewDto>> GetByMovieIdAsync(int movieId);
    Task<IEnumerable<ReviewDto>> GetByUserIdAsync(int userId);
    Task<ReviewDto?> GetUserReviewForMovieAsync(int userId, int movieId);
    
    Task<ReviewDto> CreateAsync(ReviewCreateDto dto);
    Task<ReviewDto?> UpdateAsync(int id, ReviewUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    
    // Методы для работы с рейтингом
    Task<MovieRatingDto> GetMovieRatingAsync(int movieId);
    Task<bool> UserHasReviewedMovieAsync(int userId, int movieId);
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Movie;
using ScriptumLux.BLL.DTOs.Review;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ReviewService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        var reviews = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Movie)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto?> GetByIdAsync(int id)
    {
        var review = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Movie)
            .FirstOrDefaultAsync(r => r.ReviewId == id);
            
        return review == null ? null : _mapper.Map<ReviewDto>(review);
    }

    public async Task<IEnumerable<ReviewDto>> GetByMovieIdAsync(int movieId)
    {
        var reviews = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Movie)
            .Where(r => r.MovieId == movieId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
            
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<IEnumerable<ReviewDto>> GetByUserIdAsync(int userId)
    {
        var reviews = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Movie)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
            
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto?> GetUserReviewForMovieAsync(int userId, int movieId)
    {
        var review = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Movie)
            .FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
            
        return review == null ? null : _mapper.Map<ReviewDto>(review);
    }

    public async Task<ReviewDto> CreateAsync(ReviewCreateDto dto)
    {
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.UserId == dto.UserId && r.MovieId == dto.MovieId);
            
        if (existingReview != null)
        {
            throw new InvalidOperationException("Пользователь уже оставил отзыв на этот фильм");
        }

        return await _context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var review = _mapper.Map<Review>(dto);
                review.CreatedAt = DateTime.UtcNow;
                review.UpdatedAt = DateTime.UtcNow;
                
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                await UpdateMovieRatingAsync(dto.MovieId);
                
                await transaction.CommitAsync();
                
                var createdReview = await _context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.Movie)
                    .FirstAsync(r => r.ReviewId == review.ReviewId);
                    
                return _mapper.Map<ReviewDto>(createdReview);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }

    public async Task<ReviewDto?> UpdateAsync(int id, ReviewUpdateDto dto)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return null;

        return await _context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var oldRating = review.Rating;
                
                _mapper.Map(dto, review);
                review.UpdatedAt = DateTime.UtcNow;
                
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();

                await UpdateMovieRatingAsync(review.MovieId);
                
                await transaction.CommitAsync();
                
                var updatedReview = await _context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.Movie)
                    .FirstAsync(r => r.ReviewId == id);
                    
                return _mapper.Map<ReviewDto>(updatedReview);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return false;

        return await _context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var movieId = review.MovieId;
                
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();

                await UpdateMovieRatingAsync(movieId);
                
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }

    public async Task<MovieRatingDto> GetMovieRatingAsync(int movieId)
    {
        var movie = await _context.Movies
            .Include(m => m.Reviews)
            .FirstOrDefaultAsync(m => m.MovieId == movieId);
            
        if (movie == null)
        {
            throw new ArgumentException("Фильм не найден", nameof(movieId));
        }

        var ratingDistribution = await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .GroupBy(r => r.Rating)
            .ToDictionaryAsync(g => g.Key, g => g.Count());

        return new MovieRatingDto
        {
            MovieId = movieId,
            AverageRating = movie.AverageRating,
            TotalRatings = movie.TotalRatings,
            RatingDistribution = ratingDistribution
        };
    }

    public async Task<bool> UserHasReviewedMovieAsync(int userId, int movieId)
    {
        return await _context.Reviews
            .AnyAsync(r => r.UserId == userId && r.MovieId == movieId);
    }

    private async Task UpdateMovieRatingAsync(int movieId)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.MovieId == movieId);
            
        if (movie == null) 
        {
            throw new InvalidOperationException($"Фильм с ID {movieId} не найден");
        }

        var reviews = await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .Select(r => r.Rating)
            .ToListAsync();

        if (reviews.Count == 0)
        {
            movie.AverageRating = 0;
            movie.TotalRatings = 0;
            movie.TotalRatingSum = 0;
        }
        else
        {
            movie.TotalRatings = reviews.Count;
            movie.TotalRatingSum = reviews.Sum();
            movie.AverageRating = Math.Round((decimal)movie.TotalRatingSum / movie.TotalRatings, 2);
        }
        
        _context.Entry(movie).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException($"Ошибка при обновлении рейтинга фильма {movieId}: {ex.Message}", ex);
        }
    }
}
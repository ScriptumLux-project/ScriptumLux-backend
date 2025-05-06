using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        var list = await _context.Reviews.Include(r => r.User).Include(r => r.Movie).ToListAsync();
        return _mapper.Map<IEnumerable<ReviewDto>>(list);
    }

    public async Task<ReviewDto?> GetByIdAsync(int id)
    {
        var entity = await _context.Reviews.Include(r => r.User).Include(r => r.Movie)
            .FirstOrDefaultAsync(r => r.ReviewId == id);
        if (entity == null) return null;
        return _mapper.Map<ReviewDto>(entity);
    }

    public async Task<ReviewDto> CreateAsync(ReviewCreateDto dto)
    {
        var entity = _mapper.Map<Review>(dto);
        _context.Reviews.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReviewDto>(entity);
    }

    public async Task<ReviewDto?> UpdateAsync(int id, ReviewUpdateDto dto)
    {
        var entity = await _context.Reviews.FindAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        _context.Reviews.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReviewDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Reviews.FindAsync(id);
        if (entity == null) return false;
        _context.Reviews.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.History;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services;

public class HistoryService : IHistoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public HistoryService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<HistoryDto>> GetAllAsync()
    {
        var list = await _context.HistoryRecords
            .AsNoTracking()
            .ToListAsync();
        return _mapper.Map<List<HistoryDto>>(list);
    }

    public async Task<HistoryDto> GetByIdAsync(int userId, int movieId)
    {
        var entity = await _context.HistoryRecords
            .Where(h => h.UserId == userId && h.MovieId == movieId)
            .OrderByDescending(h => h.ViewedAt)
            .FirstOrDefaultAsync();
        return entity == null ? null : _mapper.Map<HistoryDto>(entity);
    }

    public async Task<HistoryDto> CreateAsync(HistoryCreateDto dto)
    {
        var user = await _context.Users.FindAsync(dto.UserId)
            ?? throw new ArgumentException($"User {dto.UserId} not found");
        var movie = await _context.Movies.FindAsync(dto.MovieId)
            ?? throw new ArgumentException($"Movie {dto.MovieId} not found");

        var entity = _mapper.Map<History>(dto);
        entity.User = user;
        entity.Movie = movie;

        _context.HistoryRecords.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<HistoryDto>(entity);
    }

    public async Task<HistoryDto> UpdateAsync(int userId, int movieId, HistoryUpdateDto dto)
    {
        var entity = await _context.HistoryRecords
            .Where(h => h.UserId == userId && h.MovieId == movieId)
            .OrderByDescending(h => h.ViewedAt)
            .FirstOrDefaultAsync();
        if (entity == null) return null;

        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<HistoryDto>(entity);
    }

    public async Task<bool> DeleteAsync(int userId, int movieId)
    {
        var records = await _context.HistoryRecords
            .Where(h => h.UserId == userId && h.MovieId == movieId)
            .ToListAsync();
        if (!records.Any()) return false;

        _context.HistoryRecords.RemoveRange(records);
        await _context.SaveChangesAsync();
        return true;
    }

    // Новый метод для удаления всей истории пользователя
    public async Task<bool> DeleteUserHistoryAsync(int userId)
    {
        var userRecords = await _context.HistoryRecords
            .Where(h => h.UserId == userId)
            .ToListAsync();
        
        if (!userRecords.Any()) return false;

        _context.HistoryRecords.RemoveRange(userRecords);
        await _context.SaveChangesAsync();
        return true;
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Genre;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services;

public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GenreService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        var list = await _context.Genres.ToListAsync();
        return _mapper.Map<IEnumerable<GenreDto>>(list);
    }

    public async Task<GenreDto?> GetByIdAsync(int id)
    {
        var entity = await _context.Genres.FindAsync(id);
        if (entity == null) return null;
        return _mapper.Map<GenreDto>(entity);
    }

    public async Task<GenreDto> CreateAsync(GenreCreateDto dto)
    {
        var entity = _mapper.Map<Genre>(dto);
        _context.Genres.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<GenreDto>(entity);
    }

    public async Task<GenreDto?> UpdateAsync(int id, GenreUpdateDto dto)
    {
        var entity = await _context.Genres.FindAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        _context.Genres.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<GenreDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Genres.FindAsync(id);
        if (entity == null) return false;
        _context.Genres.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
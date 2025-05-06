using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Movie;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services;

public class MovieService : IMovieService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MovieService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MovieDto>> GetAllAsync()
    {
        var list = await _context.Movies.Include(m => m.Genre).ToListAsync();
        return _mapper.Map<IEnumerable<MovieDto>>(list);
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        var movie = await _context.Movies.Include(m => m.Genre)
            .FirstOrDefaultAsync(m => m.MovieId == id);
        if (movie == null) return null;
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto> CreateAsync(MovieCreateDto dto)
    {
        var entity = _mapper.Map<Movie>(dto);
        _context.Movies.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<MovieDto>(entity);
    }

    public async Task<MovieDto?> UpdateAsync(int id, MovieUpdateDto dto)
    {
        var entity = await _context.Movies.FindAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        _context.Movies.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<MovieDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Movies.FindAsync(id);
        if (entity == null) return false;
        _context.Movies.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
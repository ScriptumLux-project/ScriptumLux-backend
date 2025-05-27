using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Movie;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

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
        var movies = await _context.Movies
            .Include(m => m.Genre)
            .Include(m => m.Comments)
            .ThenInclude(c => c.User)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Genre)
            .Include(m => m.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null) return null;
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto> CreateAsync(MovieCreateDto dto)
    {
        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Name.ToLower() == dto.GenreName.ToLower());

        if (genre == null)
        {
            genre = new Genre { Name = dto.GenreName };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        var movie = _mapper.Map<Movie>(dto);
        movie.GenreId = genre.GenreId;
        
        movie.AverageRating = 0;
        movie.TotalRatings = 0;
        movie.TotalRatingSum = 0;

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        var createdMovie = await _context.Movies
            .Include(m => m.Genre)
            .Include(m => m.Comments)
            .FirstAsync(m => m.MovieId == movie.MovieId);

        return _mapper.Map<MovieDto>(createdMovie);
    }

    public async Task<MovieDto?> UpdateAsync(int id, MovieUpdateDto dto)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null)
            return null;

        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Name.ToLower() == dto.GenreName.ToLower());

        if (genre == null)
        {
            genre = new Genre { Name = dto.GenreName };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        movie.Title = dto.Title;
        movie.ReleaseYear = dto.ReleaseYear;
        movie.Country = dto.Country;
        movie.GenreId = genre.GenreId;
        movie.PosterUrl = dto.PosterUrl;
        movie.VideoUrl = dto.VideoUrl;
        movie.Description = dto.Description;

        await _context.SaveChangesAsync();

        var updatedMovie = await _context.Movies
            .Include(m => m.Genre)
            .Include(m => m.Comments)
            .ThenInclude(c => c.User)
            .FirstAsync(m => m.MovieId == id);

        return _mapper.Map<MovieDto>(updatedMovie);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return false;
        
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return true;
    }
}
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
        // Шукаємо жанр по назві
        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Name.ToLower() == dto.GenreName.ToLower());

        // Якщо жанру немає — створюємо
        if (genre == null)
        {
            genre = new Genre { Name = dto.GenreName };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync(); // зберігаємо, щоб з’явився ID
        }

        var movie = new Movie
        {
            Title = dto.Title,
            ReleaseYear = dto.ReleaseYear,
            //Rating = dto.Rating,
            Country = dto.Country,
            GenreId = genre.GenreId, // використовуємо ID знайденого/створеного жанру
            PosterUrl = dto.PosterUrl,
            VideoUrl = dto.VideoUrl,
            Description = dto.Description
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            Rating = movie.Rating,
            Country = movie.Country,
            GenreId = movie.GenreId,
            PosterUrl = movie.PosterUrl,
            VideoUrl = movie.VideoUrl,
            Description = movie.Description
        };
    }

    public async Task<MovieDto?> UpdateAsync(int id, MovieUpdateDto dto)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null)
            return null;

        // Находим или создаём жанр по имени
        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Name.ToLower() == dto.GenreName.ToLower());

        if (genre == null)
        {
            genre = new Genre { Name = dto.GenreName };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync(); // нужно для получения GenreId
        }

        // Обновляем свойства фильма
        movie.Title = dto.Title;
        movie.ReleaseYear = dto.ReleaseYear;
        //movie.Rating = dto.Rating;
        movie.Country = dto.Country;
        movie.GenreId = genre.GenreId;
        movie.PosterUrl = dto.PosterUrl;
        movie.VideoUrl = dto.VideoUrl;
        movie.Description = dto.Description;

        await _context.SaveChangesAsync();

        return new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            Rating = movie.Rating,
            Country = movie.Country,
            GenreId = movie.GenreId,
            PosterUrl = movie.PosterUrl,
            VideoUrl = movie.VideoUrl,
            Description = movie.Description
        };
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
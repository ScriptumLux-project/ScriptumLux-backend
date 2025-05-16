using Microsoft.Extensions.Options;
using ScriptumLux.BLL.AiExternal;
using ScriptumLux.BLL.DTOs.Movie;
using ScriptumLux.BLL.Interfaces;

namespace ScriptumLux.BLL.Services;

public class MovieRecommendationService : IMovieRecommendationService
{
    private readonly IMovieService _movieService;
    private readonly AISettings _aiSettings;
    private AiRecommendations _aiRecommendations;
    private readonly object _lockObject = new();
    private bool _isInitialized = false;
    private List<VectorizedMovie> _movies = new();

    public MovieRecommendationService(
        IMovieService movieService,
        IOptions<AISettings> aiSettings)
    {
        _movieService = movieService;
        _aiSettings = aiSettings.Value;
    }

    public async Task<List<MovieDto>> GetSimilarMoviesAsync(int movieId, uint maxResults = 5)
    {
        await EnsureInitializedAsync();
        
        var targetMovie = _movies.FirstOrDefault(m => m.MovieId == movieId);
        if (targetMovie == null)
        {
            // Если фильм не найден, возвращаем пустой список
            return new List<MovieDto>();
        }

        var similarMovies = await _aiRecommendations.GetSimilarItems(targetMovie, maxResults);
        
        // Преобразуем VectorizedMovie в MovieDto
        var movieDtos = new List<MovieDto>();
        foreach (var movie in similarMovies)
        {
            // Получаем полные данные о фильме из сервиса фильмов
            var fullMovieData = await _movieService.GetByIdAsync(movie.MovieId);
            if (fullMovieData != null)
            {
                movieDtos.Add(fullMovieData);
            }
        }
        
        return movieDtos;
    }

    public async Task InitializeOrUpdateMovieDatabaseAsync()
    {
        // Пересоздаем базу вне зависимости от _isInitialized
        _isInitialized = false;
        await EnsureInitializedAsync();
    }

    private async Task EnsureInitializedAsync()
    {
        if (!_isInitialized)
        {
            lock (_lockObject)
            {
                if (!_isInitialized)
                {
                    // Инициализация только один раз
                    var uri = new Uri(_aiSettings.OllamaUrl);
                    _aiRecommendations = new AiRecommendations(_movies, uri, _aiSettings.EmbeddingsModel);
                    _isInitialized = true;
                }
            }
            
            // Загружаем все фильмы
            await LoadAllMoviesAsync();
        }
    }

    private async Task LoadAllMoviesAsync()
    {
        // Загружаем все фильмы из сервиса фильмов
        var allMovies = await _movieService.GetAllAsync();
        
        // Преобразуем в VectorizedMovie
        _movies.Clear();
        foreach (var movie in allMovies)
        {
            _movies.Add(new VectorizedMovie(movie));
        }
    }
}
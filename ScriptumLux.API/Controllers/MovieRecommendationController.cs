using Microsoft.AspNetCore.Mvc;
using ScriptumLux.BLL.DTOs.Movie;
using ScriptumLux.BLL.Interfaces;

namespace ScriptumLux.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieRecommendationController : ControllerBase
{
    private readonly IMovieRecommendationService _recommendationService;
    private readonly ILogger<MovieRecommendationController> _logger;

    public MovieRecommendationController(
        IMovieRecommendationService recommendationService,
        ILogger<MovieRecommendationController> logger)
    {
        _recommendationService = recommendationService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список похожих фильмов для указанного фильма
    /// </summary>
    /// <param name="movieId">ID фильма</param>
    /// <param name="maxResults">Максимальное количество результатов (по умолчанию 5)</param>
    /// <returns>Список рекомендованных фильмов</returns>
    [HttpGet("similar/{movieId}")]
    [ProducesResponseType(typeof(List<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<MovieDto>>> GetSimilarMovies(int movieId, [FromQuery] uint maxResults = 5)
    {
        try
        {
            var recommendations = await _recommendationService.GetSimilarMoviesAsync(movieId, maxResults);
            if (recommendations == null || recommendations.Count == 0)
            {
                return NotFound($"Не найдены рекомендации для фильма с ID {movieId}");
            }
            return Ok(recommendations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении рекомендаций фильмов для ID {MovieId}", movieId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при обработке запроса");
        }
    }

    /// <summary>
    /// Обновить векторную базу данных фильмов (для администраторов)
    /// </summary>
    [HttpPost("rebuild-database")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RebuildRecommendationDatabase()
    {
        try
        {
            await _recommendationService.InitializeOrUpdateMovieDatabaseAsync();
            return Ok("База рекомендаций успешно обновлена");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении базы рекомендаций фильмов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при обработке запроса");
        }
    }
}

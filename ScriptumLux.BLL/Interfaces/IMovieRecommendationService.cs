using ScriptumLux.BLL.DTOs.Movie;

namespace ScriptumLux.BLL.Interfaces;

public interface IMovieRecommendationService
{
    /// <summary>
    /// Получает рекомендации похожих фильмов на основе указанного фильма
    /// </summary>
    /// <param name="movieId">Идентификатор фильма, для которого нужны рекомендации</param>
    /// <param name="maxResults">Максимальное количество результатов</param>
    /// <returns>Список рекомендованных фильмов</returns>
    Task<List<MovieDto>> GetSimilarMoviesAsync(int movieId, uint maxResults = 5);
    
    /// <summary>
    /// Инициализирует или обновляет векторную базу данных фильмов
    /// </summary>
    Task InitializeOrUpdateMovieDatabaseAsync();
}
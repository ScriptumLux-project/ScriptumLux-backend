using ScriptumLux.BLL.DTOs.Movie;

namespace ScriptumLux.BLL.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAllAsync();
    Task<MovieDto?> GetByIdAsync(int id);
    Task<MovieDto> CreateAsync(MovieCreateDto dto);
    Task<MovieDto?> UpdateAsync(int id, MovieUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

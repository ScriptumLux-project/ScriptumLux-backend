using ScriptumLux.BLL.DTOs.Genre;

namespace ScriptumLux.BLL.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllAsync();
    Task<GenreDto?> GetByIdAsync(int id);
    Task<GenreDto> CreateAsync(GenreCreateDto dto);
    Task<GenreDto?> UpdateAsync(int id, GenreUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
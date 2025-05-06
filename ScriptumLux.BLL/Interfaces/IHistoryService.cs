using ScriptumLux.BLL.DTOs.History;

namespace ScriptumLux.BLL.Interfaces;

public interface IHistoryService
{
    Task<IEnumerable<HistoryDto>> GetAllAsync();
    Task<HistoryDto?> GetByIdAsync(int userId, int movieId);
    Task<HistoryDto> CreateAsync(HistoryCreateDto dto);
    Task<HistoryDto?> UpdateAsync(int userId, int movieId, HistoryUpdateDto dto);
    Task<bool> DeleteAsync(int userId, int movieId);
}
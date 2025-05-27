using ScriptumLux.BLL.DTOs.Timecode;

namespace ScriptumLux.BLL.Interfaces;

public interface ITimecodeService
{
    Task<IEnumerable<TimecodeDto>> GetAllAsync();
    Task<TimecodeDto?> GetByIdAsync(int id);
    Task<TimecodeDto> CreateAsync(TimecodeCreateDto dto);
    Task<TimecodeDto?> UpdateAsync(int id, TimecodeUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
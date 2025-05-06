using ScriptumLux.BLL.DTOs.Playlist;

namespace ScriptumLux.BLL.Interfaces;

public interface IPlaylistService
{
    Task<IEnumerable<PlaylistDto>> GetAllAsync();
    Task<PlaylistDto?> GetByIdAsync(int id);
    Task<PlaylistDto> CreateAsync(PlaylistCreateDto dto);
    Task<PlaylistDto?> UpdateAsync(int id, PlaylistUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
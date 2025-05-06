using ScriptumLux.BLL.DTOs.PlaylistMovie;

namespace ScriptumLux.BLL.Interfaces;

public interface IPlaylistMovieService
{
    Task<IEnumerable<PlaylistMovieDto>> GetAllAsync();
    Task<PlaylistMovieDto?> GetByIdAsync(int playlistId, int movieId);
    Task<PlaylistMovieDto> CreateAsync(PlaylistMovieCreateDto dto);
    Task<PlaylistMovieDto?> UpdateAsync(int playlistId, int movieId, PlaylistMovieUpdateDto dto);
    Task<bool> DeleteAsync(int playlistId, int movieId);
}
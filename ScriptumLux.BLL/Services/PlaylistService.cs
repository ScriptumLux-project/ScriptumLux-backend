using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Playlist;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services;

public class PlaylistService : IPlaylistService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PlaylistService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlaylistDto>> GetAllAsync()
    {
        var list = await _context.Playlists.Include(p => p.User).ToListAsync();
        return _mapper.Map<IEnumerable<PlaylistDto>>(list);
    }

    public async Task<PlaylistDto?> GetByIdAsync(int id)
    {
        var entity = await _context.Playlists.Include(p => p.User)
            .FirstOrDefaultAsync(p => p.PlaylistId == id);
        if (entity == null) return null;
        return _mapper.Map<PlaylistDto>(entity);
    }

    public async Task<PlaylistDto> CreateAsync(PlaylistCreateDto dto)
    {
        var entity = _mapper.Map<Playlist>(dto);
        _context.Playlists.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PlaylistDto>(entity);
    }

    public async Task<PlaylistDto?> UpdateAsync(int id, PlaylistUpdateDto dto)
    {
        var entity = await _context.Playlists.FindAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        _context.Playlists.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PlaylistDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Playlists.FindAsync(id);
        if (entity == null) return false;
        _context.Playlists.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

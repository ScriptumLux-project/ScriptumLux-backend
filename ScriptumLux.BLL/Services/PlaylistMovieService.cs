using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.PlaylistMovie;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services;

    public class PlaylistMovieService : IPlaylistMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlaylistMovieService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlaylistMovieDto>> GetAllAsync()
        {
            var list = await _context.PlaylistMovies.Include(pm => pm.Playlist).Include(pm => pm.Movie).ToListAsync();
            return _mapper.Map<IEnumerable<PlaylistMovieDto>>(list);
        }

        public async Task<PlaylistMovieDto?> GetByIdAsync(int playlistId, int movieId)
        {
            var entity = await _context.PlaylistMovies
                .Include(pm => pm.Playlist)
                .Include(pm => pm.Movie)
                .FirstOrDefaultAsync(pm => pm.PlaylistId == playlistId && pm.MovieId == movieId);

            return entity == null ? null : _mapper.Map<PlaylistMovieDto>(entity);
        }


        public async Task<PlaylistMovieDto> CreateAsync(PlaylistMovieCreateDto dto)
        {
            var entity = _mapper.Map<PlaylistMovie>(dto);
            _context.PlaylistMovies.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlaylistMovieDto>(entity);
        }

        public async Task<PlaylistMovieDto?> UpdateAsync(int playlistId, int movieId, PlaylistMovieUpdateDto dto)
        {
            var entity = await _context.PlaylistMovies
                .FirstOrDefaultAsync(pm => pm.PlaylistId == playlistId && pm.MovieId == movieId);

            if (entity == null) return null;

            _mapper.Map(dto, entity);
            _context.PlaylistMovies.Update(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlaylistMovieDto>(entity);
        }


        public async Task<bool> DeleteAsync(int playlistId, int movieId)
        {
            var entity = await _context.PlaylistMovies
                .FirstOrDefaultAsync(pm => pm.PlaylistId == playlistId && pm.MovieId == movieId);

            if (entity == null) return false;

            _context.PlaylistMovies.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

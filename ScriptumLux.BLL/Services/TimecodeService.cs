using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Timecode;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services
{
    public class TimecodeService : ITimecodeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TimecodeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimecodeDto>> GetAllAsync()
        {
            var list = await _context.Timecodes
                .Include(tc => tc.User)
                .Include(tc => tc.Movie)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TimecodeDto>>(list);
        }

        public async Task<TimecodeDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Timecodes
                .Include(tc => tc.User)
                .Include(tc => tc.Movie)
                .FirstOrDefaultAsync(tc => tc.TimecodeId == id);
            return entity == null ? null : _mapper.Map<TimecodeDto>(entity);
        }

        public async Task<TimecodeDto> CreateAsync(TimecodeCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Timestamp))
                throw new ArgumentException("Timestamp is required.", nameof(dto.Timestamp));
            if (!TimeSpan.TryParse(dto.Timestamp, out var ts))
                throw new ArgumentException($"Invalid timestamp format: '{dto.Timestamp}'", nameof(dto.Timestamp));

            var entity = _mapper.Map<Timecode>(dto);
            entity.Timestamp = ts;

            _context.Timecodes.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TimecodeDto>(entity);
        }

        public async Task<TimecodeDto?> UpdateAsync(int id, TimecodeUpdateDto dto)
        {
            var entity = await _context.Timecodes.FindAsync(id);
            if (entity == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Label))
                entity.Label = dto.Label;
            if (!string.IsNullOrWhiteSpace(dto.Timestamp))
            {
                if (!TimeSpan.TryParse(dto.Timestamp, out var newTs))
                    throw new ArgumentException($"Invalid timestamp format: '{dto.Timestamp}'", nameof(dto.Timestamp));
                entity.Timestamp = newTs;
            }

            _context.Timecodes.Update(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TimecodeDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Timecodes.FindAsync(id);
            if (entity == null) return false;

            _context.Timecodes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
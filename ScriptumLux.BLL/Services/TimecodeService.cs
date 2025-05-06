using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Timecode;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services;

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
        var list = await _context.Timecodes.Include(tc => tc.User).Include(tc => tc.Movie).ToListAsync();
        return _mapper.Map<IEnumerable<TimecodeDto>>(list);
    }

    public async Task<TimecodeDto?> GetByIdAsync(int id)
    {
        var entity = await _context.Timecodes
            .Include(tc => tc.User)
            .Include(tc => tc.Movie)
            .FirstOrDefaultAsync(tc => tc.TimecodeId == id);
        if (entity == null) return null;
        return _mapper.Map<TimecodeDto>(entity);
    }

    public async Task<TimecodeDto> CreateAsync(TimecodeCreateDto dto)
    {
        var entity = _mapper.Map<Timecode>(dto);
        _context.Timecodes.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<TimecodeDto>(entity);
    }

    public async Task<TimecodeDto?> UpdateAsync(int id, TimecodeUpdateDto dto)
    {
        var entity = await _context.Timecodes.FindAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
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

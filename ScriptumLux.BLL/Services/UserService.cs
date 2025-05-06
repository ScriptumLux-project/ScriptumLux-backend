using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.User;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(UserCreateDto dto)
    {
        var entity = _mapper.Map<User>(dto);
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserDto>(entity);
    }

    public async Task<UserDto?> UpdateAsync(int id, UserUpdateDto dto)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null) return false;
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

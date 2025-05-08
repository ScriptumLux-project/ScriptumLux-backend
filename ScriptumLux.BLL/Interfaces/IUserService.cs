using ScriptumLux.BLL.DTOs.User;

namespace ScriptumLux.BLL.Interfaces;

public interface IUserService
{
    Task<UserDto[]> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(UserCreateDto dto);
    Task<UserDto?> UpdateAsync(int id, UserUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<UserDto?> AuthenticateAsync(string email, string password);
}
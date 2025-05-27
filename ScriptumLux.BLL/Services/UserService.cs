using System.Security.Cryptography;
using System.Text;
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

        public async Task<UserDto[]> GetAllAsync()
        {
            var users = await _context.Users.ToArrayAsync();
            return _mapper.Map<UserDto[]>(users);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserCreateDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new ArgumentException($"Email {dto.Email} уже используется.");

            var user = _mapper.Map<User>(dto);

            using var hmac = new HMACSHA512();
            user.PasswordSalt = Convert.ToBase64String(hmac.Key);
            user.PasswordHash = Convert.ToBase64String(
                hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            _mapper.Map(dto, user);
            if (!string.IsNullOrEmpty(dto.Password))
            {
                using var hmac = new HMACSHA512();
                user.PasswordSalt = Convert.ToBase64String(hmac.Key);
                user.PasswordHash = Convert.ToBase64String(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            var saltBytes = Convert.FromBase64String(user.PasswordSalt);
            using var hmac = new HMACSHA512(saltBytes);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            if (!Convert.ToBase64String(computedHash).Equals(user.PasswordHash))
                return null;

            return _mapper.Map<UserDto>(user);
        }
    }
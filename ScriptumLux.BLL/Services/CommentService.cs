using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Comment;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScriptumLux.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CommentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync()
        {
            var list = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Movie)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CommentDto>>(list);
        }

        public async Task<CommentDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(c => c.CommentId == id);
            return entity == null ? null : _mapper.Map<CommentDto>(entity);
        }

        public async Task<CommentDto> CreateAsync(CommentCreateDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId)
                       ?? throw new ArgumentException($"User {dto.UserId} not found");
            var movie = await _context.Movies.FindAsync(dto.MovieId)
                        ?? throw new ArgumentException($"Movie {dto.MovieId} not found");

            var comment = _mapper.Map<Comment>(dto);
            comment.User = user;
            comment.Movie = movie;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto?> UpdateAsync(int id, CommentUpdateDto dto)
        {
            var entity = await _context.Comments.FindAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CommentDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Comments.FindAsync(id);
            if (entity == null) return false;
            _context.Comments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

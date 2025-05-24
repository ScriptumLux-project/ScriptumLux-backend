using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScriptumLux.BLL.DTOs.Comment;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.DAL;
using ScriptumLux.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<CommentDto>> GetByMovieIdAsync(int movieId)
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Movie)
                .Where(c => c.MovieId == movieId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<CommentDto> CreateAsync(CommentCreateDto dto, int userId)
        {
            var user = await _context.Users.FindAsync(userId)
                       ?? throw new ArgumentException($"User {userId} not found");
            var movie = await _context.Movies.FindAsync(dto.MovieId)
                       ?? throw new ArgumentException($"Movie {dto.MovieId} not found");

            var comment = new Comment
            {
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                MovieId = dto.MovieId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var createdComment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(c => c.CommentId == comment.CommentId);

            return _mapper.Map<CommentDto>(createdComment);
        }

        public async Task<CommentDto?> UpdateAsync(int id, CommentUpdateDto dto, int userId)
        {
            var entity = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(c => c.CommentId == id);
            
            if (entity == null) return null;
            
            if (entity.UserId != userId)
                throw new UnauthorizedAccessException("You can only edit your own comments");

            entity.Content = dto.Content;
            await _context.SaveChangesAsync();
            
            return _mapper.Map<CommentDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var entity = await _context.Comments.FindAsync(id);
            if (entity == null) return false;
            
            if (entity.UserId != userId)
                throw new UnauthorizedAccessException("You can only delete your own comments");

            _context.Comments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ScriptumLux.BLL.DTOs.Comment;
using ScriptumLux.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ScriptumLux.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid user token");

            try
            {
                var created = await _service.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(Get), new { id = created.CommentId }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] CommentUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Проверяем авторизацию пользователя
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var updated = await _service.UpdateAsync(id, dto, userId);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var success = await _service.DeleteAsync(id, userId);
            return success ? NoContent() : NotFound();
        }
        
        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetByMovie(int movieId)
        {
            var list = await _service.GetByMovieIdAsync(movieId);
            return Ok(list);
        }
    }
}
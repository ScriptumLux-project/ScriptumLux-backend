using Microsoft.AspNetCore.Mvc;
using ScriptumLux.BLL.DTOs.Review;
using ScriptumLux.BLL.Interfaces;

namespace ScriptumLux.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _service;
    
    public ReviewsController(IReviewService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => OkOrNotFound(await _service.GetByIdAsync(id));

    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetByMovie(int movieId) => Ok(await _service.GetByMovieIdAsync(movieId));

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId) => Ok(await _service.GetByUserIdAsync(userId));

    [HttpGet("user/{userId}/movie/{movieId}")]
    public async Task<IActionResult> GetUserReviewForMovie(int userId, int movieId) 
        => OkOrNotFound(await _service.GetUserReviewForMovieAsync(userId, movieId));

    [HttpGet("movie/{movieId}/rating")]
    public async Task<IActionResult> GetMovieRating(int movieId)
    {
        try
        {
            var rating = await _service.GetMovieRatingAsync(movieId);
            return Ok(rating);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpGet("user/{userId}/movie/{movieId}/exists")]
    public async Task<IActionResult> CheckUserReview(int userId, int movieId)
    {
        var exists = await _service.UserHasReviewedMovieAsync(userId, movieId);
        return Ok(new { exists });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.ReviewId }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReviewUpdateDto dto) 
        => OkOrNotFound(await _service.UpdateAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) 
        => (await _service.DeleteAsync(id)) ? NoContent() : NotFound();

    private IActionResult OkOrNotFound<T>(T? result) where T : class
        => result is not null ? Ok(result) : NotFound();
}
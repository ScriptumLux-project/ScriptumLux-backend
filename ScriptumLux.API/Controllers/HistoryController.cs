using Microsoft.AspNetCore.Mvc;
using ScriptumLux.BLL.DTOs.History;
using ScriptumLux.BLL.Interfaces;

namespace ScriptumLux.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoryController : ControllerBase
{
    private readonly IHistoryService _service;
    public HistoryController(IHistoryService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{userId}/{movieId}")]
    public async Task<IActionResult> Get(int userId, int movieId) => OkOrNotFound(await _service.GetByIdAsync(userId, movieId));
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HistoryCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { userId = created.UserId, movieId = created.MovieId }, created);
    }
    [HttpPut("{userId}/{movieId}")]
    public async Task<IActionResult> Update(int userId, int movieId, [FromBody] HistoryUpdateDto dto) => OkOrNotFound(await _service.UpdateAsync(userId, movieId, dto));
    [HttpDelete("{userId}/{movieId}")]
    public async Task<IActionResult> Delete(int userId, int movieId) => (await _service.DeleteAsync(userId, movieId)) ? NoContent() : NotFound();

    private IActionResult OkOrNotFound<T>(T? result) where T : class
    {
        return result != null ? Ok(result) : NotFound();
    }
}
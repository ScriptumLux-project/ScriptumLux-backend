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
    public async Task<ActionResult<IEnumerable<HistoryDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{userId:int}/{movieId:int}")]
    public async Task<ActionResult<HistoryDto>> GetById(int userId, int movieId)
    {
        var dto = await _service.GetByIdAsync(userId, movieId);
        return dto != null ? Ok(dto) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<HistoryDto>> Create(HistoryCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { userId = created.UserId, movieId = created.MovieId }, created);
    }

    [HttpPut("{userId:int}/{movieId:int}")]
    public async Task<ActionResult<HistoryDto>> Update(int userId, int movieId, HistoryUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(userId, movieId, dto);
        return updated != null ? Ok(updated) : NotFound();
    }

    [HttpDelete("{userId:int}/{movieId:int}")]
    public async Task<IActionResult> Delete(int userId, int movieId)
        => await _service.DeleteAsync(userId, movieId) ? NoContent() : NotFound();
}
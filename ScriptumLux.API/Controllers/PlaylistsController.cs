using Microsoft.AspNetCore.Mvc;
using ScriptumLux.BLL.DTOs.Playlist;
using ScriptumLux.BLL.Interfaces;

namespace ScriptumLux.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _service;
    public PlaylistsController(IPlaylistService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => OkOrNotFound(await _service.GetByIdAsync(id));
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlaylistCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.PlaylistId }, created);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PlaylistUpdateDto dto) => OkOrNotFound(await _service.UpdateAsync(id, dto));
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => (await _service.DeleteAsync(id)) ? NoContent() : NotFound();
    
    private IActionResult OkOrNotFound<T>(T? result) where T : class
        => result is not null ? Ok(result) : NotFound();
}
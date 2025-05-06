using Microsoft.AspNetCore.Mvc;
using ScriptumLux.BLL.DTOs.PlaylistMovie;
using ScriptumLux.BLL.Interfaces;

namespace ScriptumLux.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaylistMoviesController : ControllerBase
{
    private readonly IPlaylistMovieService _service;
    public PlaylistMoviesController(IPlaylistMovieService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{playlistId}/{movieId}")]
    public async Task<IActionResult> Get(int playlistId, int movieId) =>
        OkOrNotFound(await _service.GetByIdAsync(playlistId, movieId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlaylistMovieCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { playlistId = created.PlaylistId, movieId = created.MovieId },
            created);
    }

    [HttpPut("{playlistId}/{movieId}")]
    public async Task<IActionResult> Update(int playlistId, int movieId, [FromBody] PlaylistMovieUpdateDto dto) =>
        OkOrNotFound(await _service.UpdateAsync(playlistId, movieId, dto));

    [HttpDelete("{playlistId}/{movieId}")]
    public async Task<IActionResult> Delete(int playlistId, int movieId) =>
        (await _service.DeleteAsync(playlistId, movieId)) ? NoContent() : NotFound();
    
    private IActionResult OkOrNotFound<T>(T? result) where T : class
        => result is not null ? Ok(result) : NotFound();


}
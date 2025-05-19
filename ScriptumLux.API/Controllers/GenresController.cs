using Microsoft.AspNetCore.Mvc;
using ScriptumLux.BLL.DTOs.Genre;
using ScriptumLux.BLL.Interfaces;

namespace ScriptumLux.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _service;
    public GenresController(IGenreService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() 
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var genre = await _service.GetByIdAsync(id);
        if (genre == null)
            return NotFound();
        return Ok(new { genre.GenreId, genre.Name });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GenreCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.GenreId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GenreUpdateDto dto) 
        => OkOrNotFound(await _service.UpdateAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    private IActionResult OkOrNotFound<T>(T? result) where T : class
        => result is not null ? Ok(result) : NotFound();
}

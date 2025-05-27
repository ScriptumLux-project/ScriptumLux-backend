using ScriptumLux.BLL.Interfaces;


namespace ScriptumLux.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController] 
[Route("api/ai")]
public class AiController : ControllerBase
{
    private readonly IAiService _aiService;

    public AiController(IAiService aiService)
    {
        _aiService = aiService;
    }

    [HttpGet("similar/{movieId}")]
    public async Task<IActionResult> GetSimilarMovies(int movieId, CancellationToken cancellationToken, [FromQuery] uint maxResults = 6)
    {
        var similarMovies = await _aiService.GetSimilarMovies(movieId, maxResults);

        Console.WriteLine(similarMovies.Count);

        if (similarMovies == null || similarMovies.Count == 0)
        {
            return StatusCode(202, new { message = "Рекомендации ещё не готовы" });
        }

        return Ok(similarMovies);
    }


    [HttpGet("recommendations")]
    public async Task<IActionResult> GetRecommendedMovies([FromQuery] string query, [FromQuery] uint maxResults = 2)
    {
        var recommendations = await _aiService.GetRecommendedMovies(query, maxResults);
        return Ok(recommendations);
    }
}
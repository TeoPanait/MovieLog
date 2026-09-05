using Microsoft.AspNetCore.Mvc;
using MovieLog.Services;
using MovieLog.DTOs;

namespace MovieLog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
	private readonly IStatsService _statsService;

	public StatsController(IStatsService statsService)
	{
		_statsService = statsService;
	}

    //GET: api/stats/top-rated?limit=3
	[HttpGet("top-rated")]
	public async Task<ActionResult<IEnumerable<MovieDto>>> GetTopRated([FromQuery] int limit = 3)
	{
		if(limit <= 0) limit = 3;
		var topRated = await _statsService.GetTopRatedMoviesAsync(limit, cancellationToken);
		return Ok(topRated);
	}
}

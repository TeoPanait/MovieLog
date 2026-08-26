using Microsoft.AspNetCore.Mvc;
using MovieLog.DTOs;
using MovieLog.Services;
using Microsoft.AspNetCore.Authorization;

namespace MovieLog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres(CancellationToken cancellationToken)
    {
        var genres = await _genreService.GetAllGenresAsync(cancellationToken);
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetGenre(int id, CancellationToken cancellationToken)
    {
        var genre = await _genreService.GetGenreByIdAsync(id, cancellationToken);
        if (genre == null) return NotFound();

        return Ok(genre);
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<GenreDto>> PostGenre(CreateGenreDto dto, CancellationToken cancellationToken)
    {
        var createdGenre = await _genreService.CreateGenreAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetGenres), new { id = createdGenre.Id }, createdGenre);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutGenre(int id, UpdateGenreDto dto, CancellationToken cancellationToken)
    {
        await _genreService.UpdateGenreAsync(id, dto, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteGenre(int id, CancellationToken cancellationToken)
    {
        var genre = await _genreService.GetGenreByIdAsync(id, cancellationToken);
        if (genre == null) throw new KeyNotFoundException($"Genre with ID {id} not found");

        await _genreService.DeleteGenreAsync(id, cancellationToken);
        return NoContent();
    }
}
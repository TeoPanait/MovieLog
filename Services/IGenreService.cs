using MovieLog.DTOs;
namespace MovieLog.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllGenresAsync(CancellationToken cancellationToken=default);
    Task<GenreDto?> GetGenreByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<GenreDto> CreateGenreAsync(CreateGenreDto dto, CancellationToken cancellationToken=default );
    Task UpdateGenreAsync(int id, UpdateGenreDto dto, CancellationToken cancellationToken = default);
    Task DeleteGenreAsync( int id, CancellationToken cancellationToken = default);
}

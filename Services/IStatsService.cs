using MovieLog.DTOs;

namespace MovieLog.Services;

public interface IStatsService
{
    Task<IEnumerable<TopRatedMovieDto>> GetTopRatedMoviesAsync(int limit, CancellationToken cancellationToken);
}
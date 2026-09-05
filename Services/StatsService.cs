using MovieLog.DTOs;
using MovieLog.Repositories;

namespace MovieLog.Services;

public class StatsService : IStatsService
{
    private readonly IUnitOfWork _unitOfWork;
    public StatsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<TopRatedMovieDto>> GetTopRatedMoviesAsync(int limit, CancellationToken cancellationToken)
    {
        //toate filmele cu detalii
        var movies = await _unitOfWork.MovieRepository.GetAllWithDetailsAsync(cancellationToken);

        var reviews = await _unitOfWork.ReviewRepository.GetAllAsync(cancellationToken);
        //grupare de review pe mid si calc medie
        var stats = reviews
            .GroupBy(r => r.MovieId)
            .Select(g => new
            {
                MovieId = g.Key,
                AverageRating = g.Average(r => r.Rating),
                ReviewCount = g.Count()
            })
            .OrderByDescending(s => s.AverageRating)
            .ThenByDescending(s => s.ReviewCount)
            .Take(limit)
            .ToList();
        var result = stats
            .Join(movies, s => s.MovieId,
            m => m.Id, (s, m) => new TopRatedMovieDto(
                m.Id,
                m.Title,
                m.ImageUrl,
                Math.Round(s.AverageRating, 1),
                s.ReviewCount
            ))
            .ToList();
        return result;
    }
}
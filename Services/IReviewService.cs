using MovieLog.DTOs;

namespace MovieLog.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllReviewsAsync(CancellationToken cancellationToken = default);
    Task<ReviewDto?> GetReviewByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ReviewDto>> GetReviewsForMovieAsync(int movieId, CancellationToken cancellationToken = default);
    Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto, CancellationToken cancellationToken = default);
}
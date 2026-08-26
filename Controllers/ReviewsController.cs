using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLog.Data;
using MovieLog.Models;
using MovieLog.DTOs;
using MovieLog.Services;
using Microsoft.AspNetCore.Authorization;

namespace MovieLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetAllReviewsAsync(cancellationToken);
            return Ok(reviews);
        }

        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovie(int movieId, CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetReviewsForMovieAsync(movieId, cancellationToken);
           
            return Ok(reviews);
        }

        // GET: /api/reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetReviewByIdAsync(id, cancellationToken);

            if (review == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            return Ok(review);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostReview(CreateReviewDto dto, CancellationToken cancellationToken)
        {
            var createdReview = await _reviewService.CreateReviewAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetReview), new { id = createdReview.Id }, createdReview);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, UpdateReviewDto dto, CancellationToken cancellationToken)
        {
            await _reviewService.UpdateReviewAsync(id, dto, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id, CancellationToken cancellationToken)
        {
            await _reviewService.DeleteReviewAsync(id, cancellationToken);
            return NoContent();
        }
    }
}

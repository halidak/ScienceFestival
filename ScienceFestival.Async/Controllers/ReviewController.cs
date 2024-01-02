using Microsoft.AspNetCore.Mvc;
using ScienceFestival.Async.DTOs;
using ScienceFestival.Async.Services;

namespace ScienceFestival.Async.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService reviewService;

        public ReviewController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview(ReviewDTO reviewDTO)
        {
            try
            {
                var review = await reviewService.CreateReview(reviewDTO);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-reviews")]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var reviews = await reviewService.GetAllReviews();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-reviews-for-jury/{id}")]
        public async Task<IActionResult> GetReviewsForJury(string id)
        {
            try
            {
                var reviews = await reviewService.GetReviewsForJury(id);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-reviews-for-show/{showId}")]
        public async Task<IActionResult> GetReviewsForShow(string showId)
        {
            try
            {
                var reviews = await reviewService.GetReviewsForShow(showId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
      
    }
}

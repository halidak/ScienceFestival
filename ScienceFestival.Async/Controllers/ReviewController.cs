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
                var tokenHeader = Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(tokenHeader) || !tokenHeader.ToString().StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "Invalid or missing JWT token" });
                }

                var token = tokenHeader.ToString().Substring("Bearer ".Length).Trim();

                var review = await reviewService.CreateReview(reviewDTO, token);
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

        [HttpGet("get-reviews-for-jury")]
        public async Task<IActionResult> GetReviewsForJury()
        {
            try
            {
                var tokenHeader = Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(tokenHeader) || !tokenHeader.ToString().StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "Invalid or missing JWT token" });
                }

                var token = tokenHeader.ToString().Substring("Bearer ".Length).Trim();

                var reviews = await reviewService.GetReviewsForJury(token);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ScienceFestival.REST.Gateway.DTOs;
using ScienceFestival.REST.Gateway.Models;
using ScienceFestival.REST.Gateway.Services;
using System.Net;
using System.Net.Http.Headers;

namespace ScienceFestival.REST.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly Urls urls;
        private readonly TokenService tokenService;

        public ReviewController(HttpClient httpClient, IOptions<Urls> config, TokenService service)
        {
            this.httpClient = httpClient;
            urls = config.Value;
            tokenService = service;
        }

        [HttpPost("add-review"), Authorize(Roles = "Jury")]
        public async Task<IActionResult> AddReview(ReviewRequest request)
        {
            try
            {
                var tokenHeader = Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(tokenHeader) || !tokenHeader.ToString().StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "Invalid or missing JWT token" });
                }

                var token = tokenHeader.ToString().Substring("Bearer ".Length).Trim();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var juryId = tokenService.ExtractJuryIdFromToken(token);

                var showExists = await CheckIfShowExists(request.ShowId);
                if (!showExists)
                {
                    return NotFound(new { message = "The specified show does not exist." });
                }

                var reviewDTO = new ReviewDTO
                {
                    JuryId = juryId,
                    ShowId = request.ShowId,
                    Comment = request.Comment,
                    Rating = request.Rating
                };

                var response = await httpClient.PostAsJsonAsync(urls.Reviews + "/Review/add-review", reviewDTO);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var rev = JsonConvert.DeserializeObject<Review>(content);

                return Ok(rev);
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
                var response = await httpClient.GetAsync(urls.Reviews + "/Review/get-reviews");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var reviews = JsonConvert.DeserializeObject<List<Review>>(content);

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-reviews-for-jury"), Authorize(Roles = "Jury")]
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

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var juryId = tokenService.ExtractJuryIdFromToken(token);

                var response = await httpClient.GetAsync(urls.Reviews + $"/Review/get-reviews-for-jury/{juryId}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var reviews = JsonConvert.DeserializeObject<List<Review>>(content);

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
                var response = await httpClient.GetAsync(urls.Reviews + "/Review/get-reviews-for-show/" + showId);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var reviews = JsonConvert.DeserializeObject<List<Review>>(content);

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private async Task<bool> CheckIfShowExists(string showId)
        {
            try
            {
                var response = await httpClient.GetAsync(urls.Shows + $"/show/get/{showId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ScienceFestival.REST.Gateway.DTOs;
using ScienceFestival.REST.Gateway.Models;
using ScienceFestival.REST.Gateway.Services;
using System.Net.Http.Headers;
using System.Text;

namespace ScienceFestival.REST.Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly Urls urls;
        private readonly TokenService tokenService;

        public ShowController(HttpClient httpClient, IOptions<Urls> config, TokenService tokenService)
        {
            this.httpClient = httpClient;
            urls = config.Value;
            this.tokenService = tokenService;
        }

        [HttpPost("add-show"), Authorize(Roles = "Performer")]
        public async Task<IActionResult> AddShow(ShowRequest request)
        {

            var tokenHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(tokenHeader) || !tokenHeader.ToString().StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Invalid or missing JWT token" });
            }

            var token = tokenHeader.ToString().Substring("Bearer ".Length).Trim();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var performer = tokenService.ExtractJuryIdFromToken(token);

            var show = new ShowDTO
            {
                Name = request.Name,
                ReleaseDate = request.ReleaseDate,
                Description = request.Description,
                Performer = performer
            };

            var response = httpClient.PostAsync(urls.Shows + "/show/add", new StringContent(JsonConvert.SerializeObject(show), Encoding.UTF8, "application/json")).Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var sh = JsonConvert.DeserializeObject<Show>(content);

            return Ok(sh);
        }

        [HttpGet("get-shows")]
        public async Task<IActionResult> GetShows()
        {
            var response = httpClient.GetAsync(urls.Shows + "/show/get").Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var shows = JsonConvert.DeserializeObject<List<Show>>(content);

            return Ok(shows);
        }


        [HttpGet("get-show/{id}")]
        public async Task<IActionResult> GetShowById(string id)
        {
            var response = httpClient.GetAsync(urls.Shows + "/show/get/" + id).Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var show = JsonConvert.DeserializeObject<Show>(content);

            return Ok(show);
        }

        [HttpGet("get-accepted-shows")]
        public async Task<IActionResult> GetAcceptedShows()
        {
            var response = httpClient.GetAsync(urls.Shows + "/show/get-accepted").Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var shows = JsonConvert.DeserializeObject<List<Show>>(content);

            return Ok(shows);
        }

        [HttpGet("get-unaccepted-shows")]
        public async Task<IActionResult> GetUnacceptedShows()
        {
            var response = httpClient.GetAsync(urls.Shows + "/show/get-unaccepted").Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var shows = JsonConvert.DeserializeObject<List<Show>>(content);

            return Ok(shows);
        }
       
    }
}

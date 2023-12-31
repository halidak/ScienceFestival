using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScienceFestival.REST.Gateway.Models;
using Newtonsoft.Json;
using ScienceFestival.REST.Gateway.DTOs;
using ScienceFestival.REST.Gateway.Responses;

namespace ScienceFestival.REST.Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly Urls urls;

        public UserController(HttpClient httpClient, IOptions<Urls> config)
        {
            this.httpClient = httpClient;
            urls = config.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetPerformers()
        {
            var response = httpClient.GetAsync(urls.Users + "/User/performers").Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var performers = JsonConvert.DeserializeObject<List<User>>(content);

            return Ok(performers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = httpClient.GetAsync(urls.Users + "/User/user/" + id).Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(content);

            return Ok(user);
        }

        [HttpGet("juries")]
        public async Task<IActionResult> GetJuries()
        {
            var response = httpClient.GetAsync(urls.Users + "/User/juries").Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var juries = JsonConvert.DeserializeObject<List<User>>(content);

            return Ok(juries);
        }

        //register
        [HttpPost("user-register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var response = httpClient.PostAsJsonAsync(urls.Users + "/User/user-register", userRegisterDTO).Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(content);

            return Ok(user);
        }

        //login
        [HttpPost("user-login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var response = httpClient.PostAsJsonAsync(urls.Users + "/User/user-login", userLoginDTO).Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<LoginResponse>(content);

            return Ok(user);
        }

    }
}

﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ScienceFestival.REST.Gateway.DTOs;
using ScienceFestival.REST.Gateway.Models;
using System.Text;

namespace ScienceFestival.REST.Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly Urls urls;

        public ShowController(HttpClient httpClient, IOptions<Urls> config)
        {
            this.httpClient = httpClient;
            urls = config.Value;
        }

        [HttpPost("add-show")]
        public async Task<IActionResult> AddShow(ShowDTO show)
        {
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

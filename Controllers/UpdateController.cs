using Microsoft.AspNetCore.Mvc;
using BacklogAPI.Repository;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using BacklogAPI.Models;

namespace BacklogAPI.Controllers
{


    [ApiController]
    [Route("api/refresh")]
    public class UpdateController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly UserRepository _userRepository;

        public UpdateController(IHttpClientFactory httpClientFactory, UserRepository userRepository)
        {
            _httpClient = httpClientFactory.CreateClient();
            _userRepository = userRepository;
        }

        // POST: /backlog/update/{steam_id}
        [HttpPost("{steamId}")]
        public async Task<IActionResult> UpdateBacklog(string steamId)
        {
            var user = await _userRepository.GetUserBySteamIdAsync(steamId);

            if (user == null)
            {
                return NotFound();
            }

            var dataFormat = "json";
            var steamApiKey = Environment.GetEnvironmentVariable("STEAM_API_KEY");
            string requestUrl = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={steamApiKey}&steamid={steamId}&include_appinfo=true&format={dataFormat}";

            var steamData = await _httpClient.GetFromJsonAsync<RootSteamResponse>(requestUrl);
            var games = steamData?.Response?.Games;

            return Ok(games);
        }
    }
}

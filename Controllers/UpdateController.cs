using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BacklogAPI.Data;
using BacklogAPI.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BacklogAPI.Controllers
{
    [ApiController]
    [Route("backlog/update")]
    public class UpdateController : ControllerBase
    {
        private readonly BacklogDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string _steamApiKey;

        public UpdateController(BacklogDbContext context, IMapper mapper, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClient = httpClient;
            _steamApiKey = configuration["STEAM_API_KEY"];
        }

        // POST: /backlog/update/{steam_id}
        [HttpPost("{steam_id}")]
        public async Task<IActionResult> UpdateBacklog(string steam_id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.SteamId == steam_id);

            if (user == null)
            {
                return NotFound();
            }

            var dataFormat = "json";

            var requestUrl = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={_steamApiKey}&steamid={steam_id}&include_appinfo=true&format={dataFormat}";

            var response = await _httpClient.GetStringAsync(requestUrl);


            return Ok(response);



        }
    }
}
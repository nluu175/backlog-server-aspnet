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

            var requestUrl = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={_steamApiKey}&steamid={steam_id}&include_appinfo=true&format=json";

            try
            {
                var response = await _httpClient.GetStringAsync(requestUrl);
                var steamObject = JObject.Parse(response);
                var gamesField = steamObject["response"]?["games"]?.ToObject<JArray>();

                if (gamesField == null)
                {
                    return BadRequest(new { message = "No games found for the user." });
                }

                var placeholderDescription = (string)null;
                var placeholderReleaseDate = (DateTime?)null;
                var placeholderPlatform = (string)null;
                var placeholderGenres = new[] { Guid.Parse("49941f0a-b60e-4ce0-baf2-378520c00a0a") };

                foreach (var game in gamesField)
                {
                    var appId = game["appid"].Value<int>();
                    var gameName = game["name"].Value<string>();
                    var playtimeForever = game["playtime_forever"].Value<int>();

                    var gameEntity = await _context.Games.FirstOrDefaultAsync(g => g.SteamAppId == appId);
                    if (gameEntity == null)
                    {
                        gameEntity = new Game
                        {
                            SteamAppId = appId,
                            Name = gameName,
                            Description = placeholderDescription,
                            ReleaseDate = placeholderReleaseDate,
                            Platform = placeholderPlatform
                        };
                        _context.Games.Add(gameEntity);
                        await _context.SaveChangesAsync();

                        gameEntity.Genres.AddRange(placeholderGenres.Select(g => new GameGenre { GameId = gameEntity.Id, GenreId = g }));
                        await _context.SaveChangesAsync();
                    }

                    var backlogEntryStatus = playtimeForever != 0 ? 0 : 1;
                    var backlog = await _context.Backlogs.FirstOrDefaultAsync(b => b.UserId == user.Id && b.GameId == gameEntity.Id);
                    if (backlog == null)
                    {
                        backlog = new Backlog
                        {
                            UserId = user.Id,
                            GameId = gameEntity.Id,
                            Status = backlogEntryStatus,
                            Rating = 0,
                            Comment = null,
                            Playtime = playtimeForever
                        };
                        _context.Backlogs.Add(backlog);
                    }
                    else
                    {
                        backlog.Status = 1;
                        backlog.Rating = 1;
                        backlog.Playtime = playtimeForever;
                    }
                    await _context.SaveChangesAsync();
                }

                return Ok(gamesField);
            }
            catch (HttpRequestException error)
            {
                return BadRequest(new { message = error.Message });
            }
        }
    }
}
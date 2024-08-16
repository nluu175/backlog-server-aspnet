using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BacklogAPI.Data;
using BacklogAPI.Dtos;
using BacklogAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BacklogAPI.Controllers
{
    [ApiController]
    [Route("backlog/backlogs")]
    public class BacklogController : ControllerBase
    {
        private readonly BacklogDbContext _context;
        private readonly IMapper _mapper;

        public BacklogController(BacklogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: /backlog/backlogs/{backlog_id}
        [HttpGet("{backlog_id}")]
        public async Task<IActionResult> GetBacklog(Guid backlog_id)
        {
            var backlog = await _context.Backlogs
                .Include(b => b.Game)
                .FirstOrDefaultAsync(b => b.Id == backlog_id);

            if (backlog == null)
            {
                return NotFound();
            }

            var backlogDto = _mapper.Map<BacklogDto>(backlog);
            return Ok(backlogDto);
        }

        // PUT: /backlog/backlogs/{backlog_id}
        [HttpPut("{backlog_id}")]
        public async Task<IActionResult> UpdateBacklog(Guid backlog_id, [FromBody] BacklogDto backlogDto)
        {
            var backlog = await _context.Backlogs.FindAsync(backlog_id);

            if (backlog == null)
            {
                return NotFound();
            }

            // Update fields
            backlog.Status = (Backlog.StatusTypes)backlogDto.Status;
            backlog.Rating = backlogDto.Rating;
            backlog.Comment = backlogDto.Comment;
            backlog.Playtime = (int)backlogDto.Playtime;

            await _context.SaveChangesAsync();

            var updatedBacklogDto = _mapper.Map<BacklogDto>(backlog);
            return CreatedAtAction(nameof(GetBacklog), new { backlog_id = backlog.Id }, updatedBacklogDto);
        }

        // GET: /backlog/backlogs
        [HttpGet]
        public async Task<IActionResult> GetBacklogs([FromQuery] int? page, [FromQuery] int? size)
        {
            var backlogs = _context.Backlogs.Include(b => b.Game).AsQueryable();

            if (page.HasValue && size.HasValue)
            {
                var paginatedBacklogs = await backlogs
                    .Skip((page.Value - 1) * size.Value)
                    .Take(size.Value)
                    .ToListAsync();

                var paginatedBacklogDtos = _mapper.Map<List<BacklogDto>>(paginatedBacklogs);
                return Ok(paginatedBacklogDtos);
            }

            var backlogDtos = _mapper.Map<List<BacklogDto>>(await backlogs.ToListAsync());
            return Ok(backlogDtos);
        }

        // POST: /backlog/backlogs
        [HttpPost]
        public async Task<IActionResult> CreateBacklog([FromBody] BacklogDto backlogDto)
        {
            var backlog = _mapper.Map<Backlog>(backlogDto);

            _context.Backlogs.Add(backlog);
            await _context.SaveChangesAsync();

            var createdBacklogDto = _mapper.Map<BacklogDto>(backlog);
            return CreatedAtAction(nameof(GetBacklog), new { backlog_id = backlog.Id }, createdBacklogDto);
        }
    }
}
using Calendaurus.API.Requests;
using Calendaurus.Models.Model;
using Calendaurus.Services.ProfessorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly CalendaurusContext _context;
        private readonly IProfessorService _professorService;

        public ProfessorController(CalendaurusContext context, IProfessorService professorService)
        {
            _context = context;
            _professorService = professorService;
        }

        [HttpGet("practical-events")]
        public async Task<IActionResult> GetProfessorPracticalEvents()
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Professor).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if(user.Professor is not null)
            {
                var practicalLessonEvents = await _professorService.GetProfessorPracticalEvents(user.Professor.Id);
                return Ok(practicalLessonEvents);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("practical-event/{practicalLessonId}")]
        public async Task<IActionResult> CreatePracticalLessonEvent(long practicalLessonId, [FromBody] CreatePracticalLessonEventRequest request)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Professor).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Professor is not null)
            {
                var practicalLessonEvent = await _professorService.CreatePracticalLessonEvent
                    (practicalLessonId, user.Professor.Id, request.DayOfWeek, request.StartTime, request.EndTime, request.Occurance, request.MaximumSize);
                if (practicalLessonEvent != null)
                {
                    return Ok(practicalLessonEvent);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("practical-event/{practicalLessonId}/{practicalLessonEventId}")]
        public async Task<IActionResult> UpdatePracticalLessonEvent(long practicalLessonId, long practicalLessonEventId, [FromBody] UpdatePracticalLessonEventRequest request)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Professor).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Professor is not null)
            {
                var practicalLessonEvent = _professorService.UpdatePracticalLessonEvent
                    (practicalLessonEventId, practicalLessonId , user.Professor.Id, request.DayOfWeek, request.StartTime, request.EndTime, request.Occurance, request.MaximumSize);
                if (practicalLessonEvent != null)
                {
                    return Ok(practicalLessonEvent);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("practical-event/{practicalLessonEventId}")]
        public async Task<IActionResult> DeletePracticalLessonEvent(long practicalLessonEventId)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Professor).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Professor is not null)
            {
                var practicalLessonEvent = _professorService.DeletePracticalLessonEvent(practicalLessonEventId);
                if(practicalLessonEvent != null)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("theoretical-events")]
        public async Task<IActionResult> GetProfessorTheoreticalEvents()
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Professor).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Professor is not null)
            {
                var theoreticalLessonEvents = await _professorService.GetProfessorTheoreticalEvents(user.Professor.Id);
                return Ok(theoreticalLessonEvents);
            }
            else
            {
                return Forbid();
            }
        }
    }
}

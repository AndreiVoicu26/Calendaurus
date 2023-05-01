using Calendaurus.API.Requests;
using Calendaurus.Models.Model;
using Calendaurus.Services;
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
                var practicalLessonEvents = await _context.PracticalLessonEvents.Where(e => e.ProfessorId == user.Professor.Id).ToListAsync();
                return Ok(practicalLessonEvents);
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
                var theoreticalLessonEvents = await _context.TheoreticalLessonEvents.Where(e => e.ProfessorId == user.Professor.Id).ToListAsync();
                return Ok(theoreticalLessonEvents);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("create-practical-event")]
        public async Task<IActionResult> CreatePracticalLessonEvent([FromBody] CreatePracticalLessonEventRequest request)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Professor).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Professor is not null)
            {
                var practicalLessonEvent = _professorService.CreatePracticalLessonEvent
                    (request.PracticalLessonId, user.Professor.Id, request.DayOfWeek, request.StartTime, request.EndTime, request.Occurance, request.MaximumSize);
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
    }
}

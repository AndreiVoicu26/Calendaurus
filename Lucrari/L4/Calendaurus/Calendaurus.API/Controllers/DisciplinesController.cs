using Calendaurus.API.Requests;
using Calendaurus.Models.Models;
using Calendaurus.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly CalendaurusContext _context;
        private readonly IDisciplineService _disciplineService;

        public DisciplinesController(CalendaurusContext context, IDisciplineService disciplineService)
        {
            _context = context;
            _disciplineService = disciplineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var results = await _context.Disciplines.ToListAsync();
            return Ok(results);
        }

        [HttpGet("{disciplineId}/practical")]
        public async Task<IActionResult> GetPracticalLessonsForDiscipline(long disciplineId)
        {
            var results = await _context.PracticalLessons
                    .Include(u => u.PracticalLessonEvents)
                    .Where(u => u.DisciplineId == disciplineId)
                    .AsNoTracking()
                    .ToListAsync();

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline([FromBody] CreateDisciplineRequest request)
        {
            var discipline = await _disciplineService.CreateDiscipline(request.Name, request.Faculty, request.Year);
            if (discipline != null)
            {
                return Ok(discipline);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{disciplineId}")]
        public async Task<IActionResult> UpdateDiscipline(long disciplineId, [FromBody] UpdateDisciplineRequest request)
        {
            var discipline = await _disciplineService.UpdateDiscipline(disciplineId, request.Name, request.Faculty, request.Year);
            if (discipline != null)
            {
                return Ok(discipline);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

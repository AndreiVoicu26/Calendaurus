using AutoMapper;
using Calendaurus.API.Models;
using Calendaurus.API.Requests;
using Calendaurus.Models.Models;
using Calendaurus.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.API.Controllers
{
    [Authorize]
    [ApiController]    
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly CalendaurusContext _context;
        private readonly IDisciplineService _disciplineService;
        private readonly IMapper _mapper;

        public DisciplinesController(CalendaurusContext context, IDisciplineService disciplineService, IMapper mapper)
        {
            _context = context;
            _disciplineService = disciplineService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var disciplines = await _context.Disciplines.ToListAsync();
            var results = disciplines.Select(d => _mapper.Map<DisciplineApiModel>(d));
            return Ok(results);
        }

        [HttpGet("{disciplineId}/practical")]
        public async Task<IActionResult> GetPracticalLessonsForDiscipline(long disciplineId)
        {
            var practicalLessons = await _context.PracticalLessons
                    .Include(u => u.PracticalLessonEvents)
                    .Where(u => u.DisciplineId == disciplineId)
                    .AsNoTracking()
                    .ToListAsync();

            var results = practicalLessons.Select(p => _mapper.Map<PracticalLessonApiModel>(p));
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline([FromBody] CreateDisciplineRequest request)
        {
            var discipline = await _disciplineService.CreateDiscipline(request.Name, request.Faculty, request.Year);
            if (discipline != null)
            {
                var result = _mapper.Map<DisciplineApiModel>(discipline);
                return Ok(result);
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
                var result = _mapper.Map<DisciplineApiModel>(discipline);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

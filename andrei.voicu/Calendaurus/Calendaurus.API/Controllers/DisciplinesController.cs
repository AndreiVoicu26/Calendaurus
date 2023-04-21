using Calendaurus.API.Requests;
using Calendaurus.Models.Model;
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
        private readonly IDisciplineService _service;

        public DisciplinesController(CalendaurusContext context, IDisciplineService service) 
        { 
            _context = context;
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var results = await _context.Disciplines.ToListAsync();
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline([FromBody] CreateDisciplineRequest request)
        {
            var discipline = await _service.CreateDiscipline(request.Name, request.Faculty, request.Year);

            if(discipline != null) 
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
            var discipline = await _service.UpdateDiscipline(disciplineId, request.Name, request.Faculty, request.Year);

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

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
    public class SysAdminController : ControllerBase
    {
        private readonly CalendaurusContext _context;
        private readonly IDisciplineService _service;

        public SysAdminController(CalendaurusContext context, IDisciplineService service) 
        { 
            _context = context;
            _service = service;
        }
        
        [HttpGet("disciplines")]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var disciplines = await _context.Disciplines.ToListAsync();
            return Ok(disciplines);
        }

        [HttpPost("create-discipline")]
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

        [HttpPut("update-discipline/{disciplineId}")]
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

        [HttpDelete("delete-discipline/{disciplineId}")]
        public async Task<IActionResult> DeleteDiscipline(long disciplineId)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);

            if (discipline != null)
            {
                _context.Disciplines.Remove(discipline);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("practical-lessons/{disciplineId}")]
        public async Task<IActionResult> GetAllPracticalLessonsForDiscipline(long disciplineId)
        {
            var practicalLessons = await _context.PracticalLessons.Where(pl => pl.DisciplineId == disciplineId).ToListAsync(); 
            return Ok(practicalLessons);
        }

        [HttpPost("create-practical/{disciplineId}")]
        public async Task<IActionResult> CreatePracticalLessonForDiscipline(long disciplineId, [FromBody] CreatePracticalLessonRequest request)
        {
            var practicalLesson = await _service.CreatePracticalLesson(disciplineId, request.Type, request.Description);

            if (practicalLesson != null)
            {
                return Ok(practicalLesson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update-practical/{practicalLessonId}")]
        public async Task<IActionResult> UpdatePracticalLesson(long practicalLessonId, [FromBody] UpdatePracticalLessonRequest request)
        {
            var practicalLesson = await _service.UpdatePracticalLesson(practicalLessonId, request.Type, request.Description);

            if (practicalLesson != null)
            {
                return Ok(practicalLesson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-practical/{practicalLessonId}")]
        public async Task<IActionResult> DeletePracticalLesson(long practicalLessonId)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);

            if (practicalLesson != null)
            {
                _context.PracticalLessons.Remove(practicalLesson);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("theoretical-lessons")]
        public async Task<IActionResult> GetAllTheoreticalLessons()
        {
            var theoreticalLessons = await _context.TheoreticalLessons.ToListAsync();
            return Ok(theoreticalLessons);
        }

        [HttpPost("create-theoretical/{disciplineId}")]
        public async Task<IActionResult> CreateTheoreticalLessonForDiscipline(long disciplineId, [FromBody] CreateTheoreticalLessonRequest request)
        {
            var theoreticalLesson = await _service.CreateTheoreticalLesson(disciplineId, request.Description);

            if (theoreticalLesson != null)
            {
                return Ok(theoreticalLesson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update-theoretical/{theoreticalLessonId}")]
        public async Task<IActionResult> CreateTheoreticalLessonForDiscipline(long theoreticalLessonId, [FromBody] UpdateTheoreticalLessonRequest request)
        {
            var theoreticalLesson = await _service.UpdateTheoreticalLesson(theoreticalLessonId, request.Description);

            if (theoreticalLesson != null)
            {
                return Ok(theoreticalLesson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-theoretical/{theoreticalLessonId}")]
        public async Task<IActionResult> DeleteTheroreticalLesson(long theoreticalLessonId)
        {
            var theoreticalLesson = await _context.TheoreticalLessons.FirstOrDefaultAsync(tl => tl.Id == theoreticalLessonId);

            if (theoreticalLesson != null)
            {
                _context.TheoreticalLessons.Remove(theoreticalLesson);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }




    }
}

using Calendaurus.API.Requests;
using Calendaurus.Models.Model;
using Calendaurus.Services.DisciplineService;
using Calendaurus.Services.PracticalLessonService;
using Calendaurus.Services.TheoreticalLessonService;
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
        private readonly IDisciplineService _disciplineService;
        private readonly IPracticalLessonService _practicalLessonService;
        private readonly ITheoreticalLessonService _theoreticalLessonService;

        public SysAdminController(CalendaurusContext context, IDisciplineService disciplineService, IPracticalLessonService practicalLessonService, ITheoreticalLessonService theoreticalLessonService) 
        { 
            _context = context;
            _disciplineService = disciplineService;
            _practicalLessonService = practicalLessonService;
            _theoreticalLessonService = theoreticalLessonService;
        }
        
        [HttpGet("disciplines")]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var disciplines = await _disciplineService.GetDisciplines();
            return Ok(disciplines);
        }

        [HttpPost("discipline")]
        public async Task<IActionResult> CreateDiscipline([FromBody] CreateDisciplineRequest request)
        {
            var discipline = await _disciplineService.CreateDiscipline(request.Name, request.Faculty, request.Year);

            if(discipline != null) 
            {
                return Ok(discipline);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("discipline/{disciplineId}")]
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

        [HttpDelete("discipline/{disciplineId}")]
        public async Task<IActionResult> DeleteDiscipline(long disciplineId)
        {
            var discipline = await _disciplineService.DeleteDiscipline(disciplineId);

            if (discipline != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("practical/{disciplineId}")]
        public async Task<IActionResult> GetAllPracticalLessonsForDiscipline(long disciplineId)
        {
            var practicalLessons = await _practicalLessonService.GetPracticalLessonsForDiscipline(disciplineId);
            return Ok(practicalLessons);
        }

        [HttpPost("practical/{disciplineId}")]
        public async Task<IActionResult> CreatePracticalLessonForDiscipline(long disciplineId, [FromBody] CreatePracticalLessonRequest request)
        {
            var practicalLesson = await _practicalLessonService.CreatePracticalLesson(disciplineId, request.Type, request.Description);

            if (practicalLesson != null)
            {
                return Ok(practicalLesson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("practical/{practicalLessonId}")]
        public async Task<IActionResult> UpdatePracticalLesson(long practicalLessonId, [FromBody] UpdatePracticalLessonRequest request)
        {
            var practicalLesson = await _practicalLessonService.UpdatePracticalLesson(practicalLessonId, request.Type, request.Description);

            if (practicalLesson != null)
            {
                return Ok(practicalLesson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("practical/{practicalLessonId}")]
        public async Task<IActionResult> DeletePracticalLesson(long practicalLessonId)
        {
            var practicalLesson = await _practicalLessonService.DeletePracticalLesson(practicalLessonId);

            if (practicalLesson != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("theoretical")]
        public async Task<IActionResult> GetAllTheoreticalLessons()
        {
            var theoreticalLessons = await _theoreticalLessonService.GetAllTheoreticalLessons();
            return Ok(theoreticalLessons);
        }

        [HttpPost("theoretical/{disciplineId}")]
        public async Task<IActionResult> CreateTheoreticalLessonForDiscipline(long disciplineId, [FromBody] CreateTheoreticalLessonRequest request)
        {
            var theoreticalLesson = await _theoreticalLessonService.CreateTheoreticalLesson(disciplineId, request.Description);

            if (theoreticalLesson != null)
            {
                return Ok(theoreticalLesson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("theoretical/{theoreticalLessonId}")]
        public async Task<IActionResult> CreateTheoreticalLessonForDiscipline(long theoreticalLessonId, [FromBody] UpdateTheoreticalLessonRequest request)
        {
            var theoreticalLesson = await _theoreticalLessonService.UpdateTheoreticalLesson(theoreticalLessonId, request.Description);

            if (theoreticalLesson != null)
            {
                return Ok(theoreticalLesson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("theoretical/{theoreticalLessonId}")]
        public async Task<IActionResult> DeleteTheroreticalLesson(long theoreticalLessonId)
        {
            var theoreticalLesson = await _theoreticalLessonService.DeleteTheoreticalLesson(theoreticalLessonId);

            if (theoreticalLesson != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

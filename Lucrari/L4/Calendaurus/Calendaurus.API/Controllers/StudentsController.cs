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
    public class StudentsController : ControllerBase
    {
        private readonly CalendaurusContext _context;
        private readonly IDisciplineService _disciplineService;

        public StudentsController(CalendaurusContext context, IDisciplineService disciplineService)
        {
            _context = context;
            _disciplineService = disciplineService;
        }

        [HttpGet("disciplines")]
        public async Task<IActionResult> GetStudentDisciplines()
        {
            return Ok();
        }

        [HttpGet("practical")]
        public async Task<IActionResult> GetStudentPracticalLabs()
        {
            return Ok();
        }

        [HttpGet("enrollments")]
        public async Task<IActionResult> GetStudentPracticalLabEnrollments()
        {
            return Ok();
        }

        [HttpPost("enrollments")]
        public async Task<IActionResult> CreateStudentPracticalLabEnrollment([FromBody] EnrollStudentRequest request)
        {
            return Ok();
        }

        [HttpDelete("enrollments/{practicalLessonEventId}")]
        public async Task<IActionResult> DeleteStudentPracticalLabEnrollment(long practicalLessonEventId)
        {
            return Ok();
        }

        #region old endpoints
        [HttpGet("{studentId}/disciplines")]
        public async Task<IActionResult> GetStudentDisciplines(long studentId)
        {
            var results = new List<Discipline>();

            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student != null)
            {
                var year = student.Year;
                var disciplines = await _context.Disciplines.Where(d => d.Year == year).ToListAsync();
                results = disciplines;
            }

            return Ok(results);
        }

        [HttpGet("{studentId}/practical")]
        public async Task<IActionResult> ViewEnrollmentsForStudent(long studentId)
        {
            var studentWithLabs = await _context.Students
                .Include(s => s.PracticalLessonEvens)
                .Where(s => s.Id == studentId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var practicalLessons = studentWithLabs?.PracticalLessonEvens?.ToList() ?? new List<PracticalLessonEvent>();

            return Ok(practicalLessons);
        }

        [HttpPost("{studentId}/practical")]
        public async Task<IActionResult> EnrollStudentForPracticalLessonEvent(long studentId, [FromBody] EnrollStudentRequest request)
        {
            var success = await _disciplineService.EnrollStudentToPracticalLessonEvent(studentId, request.PracticalLessonEventId);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{studentId}/practical/{practicalLessonId}")]
        public async Task<IActionResult> RemoveStudentPractical(long studentId, long practicalLessonId)
        {
            var success = await _disciplineService.RemoveStudentEnrollment(studentId, practicalLessonId);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}

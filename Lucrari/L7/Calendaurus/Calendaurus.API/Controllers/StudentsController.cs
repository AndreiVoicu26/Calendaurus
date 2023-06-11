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
    public class StudentsController : ControllerBase
    {
        private readonly CalendaurusContext _context;
        private readonly IDisciplineService _disciplineService;
        private readonly IMapper _mapper;

        public StudentsController(CalendaurusContext context, IDisciplineService disciplineService, IMapper mapper)
        {
            _context = context;
            _disciplineService = disciplineService;
            _mapper = mapper;
        }

        [HttpGet("disciplines")]
        public async Task<IActionResult> GetDisciplines()
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.User.Email == User.Identity.Name)
                .SingleOrDefaultAsync();

            if (student is not null)
            {
                var disciplines = await _context.Disciplines
                .Where(d => d.Year == student.Year)
                .ToListAsync();

                var results = disciplines.Select(_mapper.Map<DisciplineApiModel>);
                return Ok(results);
            }

            return BadRequest();
        }

        [HttpGet("practical")]
        public async Task<IActionResult> GetPracticalLabs()
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.User.Email == User.Identity.Name)
                .SingleOrDefaultAsync();

            if (student is not null)
            {
                var discipline = await _context.Disciplines
                    .Include(d => d.PracticalLessons)
                    .ThenInclude(lessons => lessons.PracticalLessonEvents)
                    .Where(d => d.Year == student.Year)
                    .ToListAsync();

                var practical = discipline.SelectMany(d => d.PracticalLessons);
                var results = practical.Select(_mapper.Map<PracticalLessonApiModel>);
                return Ok(results);
            }

            return BadRequest();
        }

        [HttpGet("enrollments")]
        public async Task<IActionResult> GetEnrollments()
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.User.Email == User.Identity.Name)
                .SingleOrDefaultAsync();

            if (student is not null)
            {
                var enrolledStudent = await _context.Students
                    .Include(s => s.PracticalLessonEvens)
                    .ThenInclude(e => e.PracticalLesson)
                    .Where(s => s.Id == student.Id)
                    .SingleOrDefaultAsync();

                var results = enrolledStudent.PracticalLessonEvens.Select(e => e.PracticalLesson).Select(_mapper
                    .Map<PracticalLessonApiModel>);
                return Ok(results);
            }

            return BadRequest();
        }

        [HttpPost("enrollments")]
        public async Task<IActionResult> CreateStudentAttendance([FromBody] EnrollStudentRequest request)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.User.Email == User.Identity.Name)
                .SingleOrDefaultAsync();

            if (student is not null)
            {
                var result = await _disciplineService.EnrollStudentToPracticalLessonEventAsync(student, request.PracticalLessonEventId);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpDelete("enrollments/{practicalLessonId}")]
        public async Task<IActionResult> DeleteStudentAttendance(long practicalLessonId)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.User.Email == User.Identity.Name)
                .SingleOrDefaultAsync();

            if (student is not null)
            {
                await _disciplineService.RemoveStudentEnrollment(student, practicalLessonId);
                return Ok();
            }

            return BadRequest();
        }
    }
}

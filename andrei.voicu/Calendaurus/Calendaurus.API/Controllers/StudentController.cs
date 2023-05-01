using Azure.Core;
using Calendaurus.API.Requests;
using Calendaurus.Models.Model;
using Calendaurus.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Calendaurus.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly CalendaurusContext _context;
        private readonly IStudentService _studentService;

        public StudentController(CalendaurusContext context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;
        }

        [HttpGet("disciplines")]
        public async Task<IActionResult> GetStudentDisciplines()
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if(user.Student is not null)
            {
                var disciplines = await _context.Disciplines.Where(d => d.Year  == user.Student.Year).ToListAsync();
                return Ok(disciplines);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("practical-lessons/{disciplineId}")]
        public async Task<IActionResult> GetStudentPracticalLessonsForDiscipline(long disciplineId)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Student is not null)
            {
                var discipline = await _context.Disciplines.Where(d => d.Year == user.Student.Year).FirstOrDefaultAsync(d => d.Id == disciplineId);
                if(discipline != null)
                {
                    var practicalLessons = await _context.PracticalLessons.Where(pl => pl.DisciplineId == disciplineId).ToListAsync();
                    return Ok(practicalLessons);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("practical-lesson-events/{practicalLessonId}")]
        public async Task<IActionResult> GetPracticalLessonEventsForPracticalLesson(long practicalLessonId)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if(user.Student is not null)
            {
                var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);
                if(practicalLesson != null) 
                {
                    var discipline = await _context.Disciplines.Where(d => d.Year == user.Student.Year).FirstOrDefaultAsync(d => d.PracticalLessons.Contains(practicalLesson));
                    if (discipline != null)
                    {
                        var practicalLessonEvents = await _context.PracticalLessonEvents.Where(e => e.PracticalLessonId == practicalLessonId).ToListAsync();
                        return Ok(practicalLessonEvents);
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("practical-enrollments")]
        public async Task<IActionResult> GetStudentPracticalLessonsEnrollments()
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if(user.Student is not null)
            {
                var enrollments = await _context.PracticalLessonEvents.Include(e => e.Students).Where(e => e.Students.Contains(user.Student)).ToListAsync();
                return Ok(enrollments);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("create-practical-enrollment/{practicalLessonId}")]
        public async Task<IActionResult> CreateStudentPracticalLessonEnrollment(long practicalLessonId, [FromBody] PracticalEnrollStudentRequest request)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Student is not null)
            {
                var success = await _studentService.EnrollStudentToPracticalLessonEvent(user.Student.Id, practicalLessonId, request.PracticalLessonEventId);
                if (success) 
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

        [HttpDelete("delete-practical-enrollment/{practicalLessonId}/{practicalLessonEventId}")]
        public async Task<IActionResult> DeleteStudentPracticalLessonEnrollment(long practicalLessonId, long practicalLessonEventId)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Student is not null)
            {
                var success = await _studentService.RemoveStudentPracticalEnrollment(user.Student.Id, practicalLessonId, practicalLessonEventId);
                if (success)
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

        [HttpGet("theoretical-lesson/{disciplineId}")]
        public async Task<IActionResult> GetStudentTheoreticalLessonForDiscipline(long disciplineId)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Student is not null)
            {
                var discipline = await _context.Disciplines.Where(d => d.Year == user.Student.Year).FirstOrDefaultAsync(d => d.Id == disciplineId);
                if (discipline != null)
                {
                    var theoreticalLesson = await _context.TheoreticalLessons.FirstOrDefaultAsync(tl => tl.DisciplineId == disciplineId);
                    return Ok(theoreticalLesson);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Forbid();
            }
        }


        [HttpGet("theoretical-lesson-events/{theoreticalLessonId}")]
        public async Task<IActionResult> GetTheoreticalLessonEventsForTheoreticalLesson(long theoreticalLessonId)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Student is not null)
            {
                var theoreticalLesson = await _context.TheoreticalLessons.FirstOrDefaultAsync(tl => tl.Id == theoreticalLessonId);
                if (theoreticalLesson != null)
                {
                    var discipline = await _context.Disciplines.Where(d => d.Year == user.Student.Year).FirstOrDefaultAsync(d => d.TheoreticalLesson == theoreticalLesson);
                    if (discipline != null)
                    {
                        var theoreticalLessonEvents = await _context.TheoreticalLessonEvents.Where(e => e.TheoreticalLessonId == theoreticalLessonId).ToListAsync();
                        return Ok(theoreticalLessonEvents);
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("theoretical-enrollments")]
        public async Task<IActionResult> GetStudentTheoreticalLessonsEnrollments()
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Student is not null)
            {
                var enrollments = await _context.TheoreticalLessonEvents.Include(e => e.Students).Where(e => e.Students.Contains(user.Student)).ToListAsync();
                return Ok(enrollments);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("create-theoretical-enrollment/{theoreticalLessonId}")]
        public async Task<IActionResult> CreateStudentTheoreticalLessonEnrollment(long theoreticalLessonId, [FromBody] TheoreticalEnrollStudentRequest request)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Student is not null)
            {
                var success = await _studentService.EnrollStudentToTheoreticalLessonEvent(user.Student.Id, theoreticalLessonId, request.TheoreticalLessonEventId);
                if (success)
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

        [HttpDelete("delete-theoretical-enrollment/{theoreticalLessonId}/{theoreticalLessonEventId}")]
        public async Task<IActionResult> DeleteStudentTheoreticalLessonEnrollment(long theoreticalLessonId, long theoreticalLessonEventId)
        {
            var userEmail = HttpContext.User.Identity.Name;

            var user = await _context.Users.Include(s => s.Student).Where(u => u.Email == userEmail).SingleOrDefaultAsync();

            if (user.Student is not null)
            {
                var success = await _studentService.RemoveStudentTheoreticalEnrollment(user.Student.Id, theoreticalLessonId, theoreticalLessonEventId);
                if (success)
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

        #region old endpoints
        /* [HttpGet("{studentId}/disciplines")]
         public async Task<IActionResult> ViewStudentDisciplines(long studentId)
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
                 .Include(s => s.PracticalLessonEvents)
                 .Where(s => s.Id == studentId)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

             var practicalLessonsEvents = studentWithLabs?.PracticalLessonEvents?.ToList() ?? new List<PracticalLessonEvent>();

             return Ok(practicalLessonsEvents);
         }

         [HttpPost("{studentId}/practical/{practicalLessonId}")]
         public async Task<IActionResult> EnrollStudentForPracticalLessonEvent(long studentId, long practicalLessonId, [FromBody] EnrollStudentRequest request)
         {
             var success = await _studentService.EnrollStudentToPracticalLessonEvent(studentId, practicalLessonId, request.PracticalLessonEventId);

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
             var success = await _studentService.RemoveStudentEnrollment(studentId, practicalLessonId);

             if (success)
             {
                 return Ok();
             }
             else
             {
                 return BadRequest();
             }
         }*/
        #endregion
    }
}
﻿using Calendaurus.API.Requests;
using Calendaurus.Models.Models;
using Calendaurus.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.API.Controllers
{
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
    }
}
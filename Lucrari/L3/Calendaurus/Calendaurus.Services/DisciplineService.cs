using Calendaurus.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly CalendaurusContext _context;

        public DisciplineService(CalendaurusContext context)
        {
            _context = context;
        }

        public async Task<Discipline> CreateDiscipline(string name, string faculty, int year)
        {
            var discipline = new Discipline()
            {
                Name = name,
                Faculty = faculty,
                Year = (byte)year
            };

            _context.Add(discipline);
            await _context.SaveChangesAsync();

            return discipline;
        }

        public async Task<bool> EnrollStudentToPracticalLessonEvent(long studentId, long practicalLessonEventId)
        {
            var practicalLesson = await _context.PracticalLessonEvents.FirstOrDefaultAsync(u => u.Id == practicalLessonEventId);
            if (practicalLesson != null) 
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
                if (student != null)
                {
                    practicalLesson.Students.Add(student);
                    await _context.SaveChangesAsync();
                    return true;
                }                
            }

            return false;
        }

        public async Task<bool> RemoveStudentEnrollment(long studentId, long enrollmentId)
        {
            var practicalLesson = await _context.PracticalLessonEvents.FirstOrDefaultAsync(u => u.Id == enrollmentId);
            if (practicalLesson != null)
            {
                var studentEnrollment = practicalLesson.Students.Where(s => s.Id == studentId).FirstOrDefault();
                if (studentEnrollment != null)
                {
                    practicalLesson.Students.Remove(studentEnrollment);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        public async Task<Discipline?> UpdateDiscipline(long id, string name, string faculty, int year)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == id);
            if (discipline != null)
            {
                discipline.Name = name;
                discipline.Faculty = faculty;
                discipline.Year = (byte)year;

                await _context.SaveChangesAsync();
            };

            return discipline;
        }
    }
}

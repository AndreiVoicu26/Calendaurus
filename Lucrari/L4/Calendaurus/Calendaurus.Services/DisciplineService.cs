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

        public async Task<bool> EnrollStudentToPracticalLessonEventAsync(Student student, long practicalLessonEventId)
        {
            if (student is null)
            {
                return false;
            }

            var practicalLesson = await _context.PracticalLessonEvents
                .Include(e => e.PracticalLesson)
                .ThenInclude(l => l.Discipline)
                .FirstOrDefaultAsync(u => u.Id == practicalLessonEventId);

            if (practicalLesson != null) 
            {
                if (practicalLesson.PracticalLesson.Discipline.Year != student.Year)
                {
                    return false;
                }

                practicalLesson.Students.Add(student);
                await _context.SaveChangesAsync();
                return true;                
            }

            return false;
        }

        public async Task<bool> RemoveStudentEnrollment(Student student, long practicalLessonId)
        {
            if (student is null)
            {
                return false;
            }

            var practicalLesson = await _context.PracticalLessonEvents
                .Include(p => p.Students)
                .FirstOrDefaultAsync(u => u.Id == practicalLessonId);

            if (practicalLesson != null)
            {
                if (practicalLesson.Students.Any(s => s.Id == student.Id))
                {
                    practicalLesson.Students.Remove(student);
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

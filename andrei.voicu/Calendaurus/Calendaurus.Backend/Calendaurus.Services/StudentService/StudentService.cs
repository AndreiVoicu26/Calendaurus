using Calendaurus.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly CalendaurusContext _context;

        public StudentService(CalendaurusContext context)
        {
            _context = context;
        }

        public async Task<List<Discipline>> GetStudentDisciplines(int year)
        {
            var disciplines = await _context.Disciplines.Where(d => d.Year == year).ToListAsync();
            return disciplines;
        }

        public async Task<List<PracticalLesson>> GetStudentPracticalLessonsForDiscipline(int year, long disciplineId)
        {
            var discipline = await _context.Disciplines.Where(d => d.Year == year).FirstOrDefaultAsync(d => d.Id == disciplineId);
            if (discipline != null)
            {
                var practicalLessons = await _context.PracticalLessons.Where(pl => pl.DisciplineId == disciplineId).ToListAsync();
                return practicalLessons;
            }
            return null;
        }

        public async Task<List<PracticalLessonEvent>> GetStudentPracticalLessonsEventsForPracticalLesson(int year, long practicalLessonId)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);
            if (practicalLesson != null)
            {
                var discipline = await _context.Disciplines.Where(d => d.Year == year).FirstOrDefaultAsync(d => d.PracticalLessons.Contains(practicalLesson));
                if (practicalLesson != null)
                {
                    if (discipline != null)
                    {
                        var practicalLessonEvents = await _context.PracticalLessonEvents.Where(e => e.PracticalLessonId == practicalLessonId).ToListAsync();
                        return practicalLessonEvents;
                    }
                }
            }
            return null;
        }

        public async Task<List<PracticalLessonEvent>> GetStudentPracticalLessonEnrollments(User user)
        {
            var enrollments = await _context.PracticalLessonEvents.Include(e => e.Students).Where(e => e.Students.Contains(user.Student)).ToListAsync();
            return enrollments;
        }

        public async Task<bool> EnrollStudentToPracticalLessonEvent(long studentId, long practicalLessonId, long practicalLessonEventId)
        {
            var practicalLessonEvent = await _context.PracticalLessonEvents.FirstOrDefaultAsync(e => e.Id == practicalLessonEventId);
            if (practicalLessonEvent != null)
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
                if (student != null && practicalLessonEvent.PracticalLessonId == practicalLessonId)
                {
                    practicalLessonEvent.Students.Add(student);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RemoveStudentPracticalEnrollment(long studentId, long practicalLessonId, long practicalLesonEventId)
        {
            var practicalLessonEvent = await _context.PracticalLessonEvents.FirstOrDefaultAsync(e => e.Id == practicalLesonEventId);
            if (practicalLessonEvent != null)
            {
                var student = await _context.Students.Include(s => s.PracticalLessonEvents).
                    FirstOrDefaultAsync(s => s.Id == studentId && s.PracticalLessonEvents.Contains(practicalLessonEvent));
                if (student != null && practicalLessonEvent.PracticalLessonId == practicalLessonId)
                {
                    practicalLessonEvent.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<TheoreticalLesson> GetStudentTheoreticalLessonForDiscipline(int year, long disciplineId)
        {
            var discipline = await _context.Disciplines.Where(d => d.Year == year).FirstOrDefaultAsync(d => d.Id == disciplineId);
            if (discipline != null)
            {
                var theoreticalLesson = await _context.TheoreticalLessons.FirstOrDefaultAsync(tl => tl.DisciplineId == disciplineId);
                return theoreticalLesson;
            }
            return null;
        }

        public async Task<List<TheoreticalLessonEvent>> GetStudentTheoreticalLessonEventsForTheoreticalLesson(int year, long theoreticalLessonId)
        {
            var theoreticalLesson = await _context.TheoreticalLessons.FirstOrDefaultAsync(tl => tl.Id == theoreticalLessonId);
            if (theoreticalLesson != null)
            {
                var discipline = await _context.Disciplines.Where(d => d.Year == year).FirstOrDefaultAsync(d => d.TheoreticalLesson == theoreticalLesson);
                if (discipline != null)
                {
                    var theoreticalLessonEvents = await _context.TheoreticalLessonEvents.Where(e => e.TheoreticalLessonId == theoreticalLessonId).ToListAsync();
                    return theoreticalLessonEvents;
                }
            }
            return null;
        }

        public async Task<List<TheoreticalLessonEvent>> GetStudentTheoreticalLessonEnrollments(User user)
        {
            var enrollments = await _context.TheoreticalLessonEvents.Include(e => e.Students).Where(e => e.Students.Contains(user.Student)).ToListAsync();
            return enrollments;
        }

        public async Task<bool> EnrollStudentToTheoreticalLessonEvent(long studentId, long theoreticalLessonId, long theoreticalLessonEventId)
        {
            var theoreticalLessonEvent = await _context.TheoreticalLessonEvents.FirstOrDefaultAsync(e => e.Id == theoreticalLessonEventId);
            if (theoreticalLessonEvent != null)
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
                if (student != null && theoreticalLessonEvent.TheoreticalLessonId == theoreticalLessonId)
                {
                    theoreticalLessonEvent.Students.Add(student);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RemoveStudentTheoreticalEnrollment(long studentId, long theoreticalLessonId, long theoreticalLessonEventId)
        {
            var theoreticalLessonEvent = await _context.TheoreticalLessonEvents.FirstOrDefaultAsync(e => e.Id == theoreticalLessonEventId);
            if (theoreticalLessonEvent != null)
            {
                var student = await _context.Students.Include(s => s.TheoreticalLessonEvents).
                    FirstOrDefaultAsync(s => s.Id == studentId && s.TheoreticalLessonEvents.Contains(theoreticalLessonEvent));
                if (student != null && theoreticalLessonEvent.TheoreticalLessonId == theoreticalLessonId)
                {
                    theoreticalLessonEvent.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
    }
}

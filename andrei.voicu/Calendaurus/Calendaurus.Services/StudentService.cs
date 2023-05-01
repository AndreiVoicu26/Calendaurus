using Calendaurus.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services
{
    public class StudentService : IStudentService
    {
        private readonly CalendaurusContext _context;

        public StudentService(CalendaurusContext context)
        {
            _context = context;
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

using Calendaurus.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.InteropServices.JavaScript;
using Calendaurus.Services.Student;

namespace Calendaurus.Services.ProfessorService
{
    public class ProfessorService : IProfessorService
    {
        private readonly CalendaurusContext _context;
        private readonly IStudentService _studentService;

        public ProfessorService(CalendaurusContext context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;
        }

        public async Task<List<PracticalLessonEvent>> GetProfessorPracticalEvents(long professorId)
        {
            var practicalLessonEvents = await _context.PracticalLessonEvents.Include(e => e.Students).Where(e => e.ProfessorId == professorId).ToListAsync();
            return practicalLessonEvents;
        }

        public async Task<PracticalLessonEvent> CreatePracticalLessonEvent(long practicalLessonId, long professorId, int day, TimeSpan startTime, TimeSpan endTime, int occurance, int size)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);
            if (practicalLesson != null)
            {
                var practicalLessonEvent = new PracticalLessonEvent()
                {
                    PracticalLessonId = practicalLessonId,
                    ProfessorId = professorId,
                    DayOfWeek = (byte)day,
                    StartTime = startTime,
                    EndTime = endTime,
                    Occurance = (byte)occurance,
                    MaximumSize = (byte)size
                };
                practicalLesson.PracticalLessonEvents.Add(practicalLessonEvent);
                _context.PracticalLessonEvents.Add(practicalLessonEvent);
                await _context.SaveChangesAsync();
                return practicalLessonEvent;
            }
            return null;
        }

        public async Task<PracticalLessonEvent> UpdatePracticalLessonEvent(long practicalLessonEventId, long practicalLessonId, long professorId, int day, TimeSpan startTime, TimeSpan endTime, int occurance, int size)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);
            if (practicalLesson != null)
            {
                var practicalLessonEvent = await _context.PracticalLessonEvents.FirstOrDefaultAsync(e => e.Id == practicalLessonEventId);
                if (practicalLessonEvent != null)
                {
                    practicalLessonEvent.ProfessorId = professorId;
                    practicalLessonEvent.DayOfWeek = (byte)day;
                    practicalLessonEvent.StartTime = startTime;
                    practicalLessonEvent.EndTime = endTime;
                    practicalLessonEvent.Occurance = (byte)occurance;
                    practicalLessonEvent.MaximumSize = (byte)size;

                    await _context.SaveChangesAsync();
                    return practicalLessonEvent;
                }
            }
            return null;
        }

        public async Task<PracticalLessonEvent> DeletePracticalLessonEvent(long practicalLessonEventId)
        {
            var practicalLessonEvent = await _context.PracticalLessonEvents.FirstOrDefaultAsync(e => e.Id == practicalLessonEventId);
            if(practicalLessonEvent != null)
            {
                //var students = await _context.Students.Where(s => s.PracticalLessonEvents.Contains(practicalLessonEvent)).ToListAsync();
                //foreach(var s in students)
                //{
                  // await _studentService.RemoveStudentPracticalEnrollment(s.Id, practicalLessonEvent.PracticalLessonId, practicalLessonEventId);
                //}
                _context.PracticalLessonEvents.Remove(practicalLessonEvent);
                await _context.SaveChangesAsync();
                return practicalLessonEvent;
            }
            return null;
        }

        public async Task<List<TheoreticalLessonEvent>> GetProfessorTheoreticalEvents(long professorId)
        {
            var theoreticalLessonEvents = await _context.TheoreticalLessonEvents.Where(e => e.ProfessorId == professorId).ToListAsync();
            return theoreticalLessonEvents;
        }



    }
}

using Calendaurus.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly CalendaurusContext _context;

        public ProfessorService(CalendaurusContext context)
        {
            _context = context;
        }

        public async Task<PracticalLessonEvent> CreatePracticalLessonEvent(long practicalLessonId, long professorId, string day, TimeSpan startTime, TimeSpan endTime, int occurance, int size)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);
            if(practicalLesson != null)
            {
                var practicalLessonEvent = new PracticalLessonEvent()
                {
                    PracticalLessonId = practicalLessonId,
                    ProfessorId = professorId,
                    DayOfWeek = day,
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

        public async Task<PracticalLessonEvent> UpdatePracticalLessonEvent(long practicalLessonEventId, long practicalLessonId, long professorId, string day, TimeSpan startTime, TimeSpan endTime, int occurance, int size)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);
            if(practicalLesson != null)
            {
                var practicalLessonEvent = await _context.PracticalLessonEvents.FirstOrDefaultAsync(e => e.Id == practicalLessonEventId);
                if (practicalLessonEvent != null)
                {
                    practicalLessonEvent.ProfessorId = professorId;
                    practicalLessonEvent.DayOfWeek = day;
                    practicalLessonEvent.StartTime = startTime;
                    practicalLessonEvent.EndTime = endTime;
                    practicalLessonEvent.Occurance = (byte) occurance;
                    practicalLessonEvent.MaximumSize = (byte) size;

                    await _context.SaveChangesAsync();
                    return practicalLessonEvent;
                }
            }
            return null;
        }
    }
}

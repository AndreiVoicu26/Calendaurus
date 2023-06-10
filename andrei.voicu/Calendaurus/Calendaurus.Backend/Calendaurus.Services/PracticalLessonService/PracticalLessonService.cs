using Calendaurus.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services.PracticalLessonService
{
    public class PracticalLessonService : IPracticalLessonService
    {
        private readonly CalendaurusContext _context;

        public PracticalLessonService(CalendaurusContext context)
        {
            _context = context;
        }

        public async Task<List<PracticalLesson>> GetPracticalLessonsForDiscipline(long disciplineId)
        {
            var practicalLessons = await _context.PracticalLessons.Include(pl => pl.PracticalLessonEvents).Where(pl => pl.DisciplineId == disciplineId).ToListAsync();
            return practicalLessons;
        }

        public async Task<PracticalLesson> CreatePracticalLesson(long disciplineId, int type, string description)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);

            if (discipline != null)
            {
                var practicalLesson = new PracticalLesson()
                {
                    Type = (byte)type,
                    Description = description,
                    DisciplineId = disciplineId
                };

                discipline.PracticalLessons.Add(practicalLesson);
                _context.PracticalLessons.Add(practicalLesson);
                await _context.SaveChangesAsync();
                return practicalLesson;
            }
            return null;
        }

        public async Task<PracticalLesson> UpdatePracticalLesson(long practicalLessonId, int type, string description)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);

            if (practicalLesson != null)
            {
                practicalLesson.Type = (byte)type;
                practicalLesson.Description = description;

                await _context.SaveChangesAsync();
            };

            return practicalLesson;
        }

        public async Task<PracticalLesson> DeletePracticalLesson(long practicalLessonId)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);

            if (practicalLesson != null)
            {
                var practicalLessonEvents = await _context.PracticalLessonEvents.Where(e => e.PracticalLessonId == practicalLessonId).ToListAsync();
                if(practicalLessonEvents != null)
                {
                    foreach (var e in practicalLessonEvents)
                    {
                        //delete each practicalLessonEvent
                    }
                    practicalLesson.PracticalLessonEvents.Clear();
                }

                _context.PracticalLessons.Remove(practicalLesson);
                await _context.SaveChangesAsync();
            }

            return practicalLesson;
        }
    }
}

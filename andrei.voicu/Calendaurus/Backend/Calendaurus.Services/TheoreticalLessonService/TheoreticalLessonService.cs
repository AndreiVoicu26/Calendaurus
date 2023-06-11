using Calendaurus.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services.TheoreticalLessonService
{
    public class TheoreticalLessonService : ITheoreticalLessonService
    {
        private readonly CalendaurusContext _context;

        public TheoreticalLessonService(CalendaurusContext context)
        {
            _context = context;
        }

        public async Task<List<TheoreticalLesson>> GetAllTheoreticalLessons()
        {
            var theoreticalLessons = await _context.TheoreticalLessons.ToListAsync();
            return theoreticalLessons;
        }

        public async Task<TheoreticalLesson> CreateTheoreticalLesson(long disciplineId, string description)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);

            if (discipline != null)
            {
                var theoreticalLesson = new TheoreticalLesson()
                {
                    Description = description,
                    DisciplineId = disciplineId,
                };

                discipline.TheoreticalLesson = theoreticalLesson;
                _context.TheoreticalLessons.Add(theoreticalLesson);
                await _context.SaveChangesAsync();
                return theoreticalLesson;
            }
            return null;
        }

        public async Task<TheoreticalLesson> UpdateTheoreticalLesson(long theoreticalLessonId, string description)
        {
            var theoreticalLesson = await _context.TheoreticalLessons.FirstOrDefaultAsync(tl => tl.Id == theoreticalLessonId);

            if (theoreticalLesson != null)
            {
                theoreticalLesson.Description = description;

                await _context.SaveChangesAsync();
            };

            return theoreticalLesson;
        }

        public async Task<TheoreticalLesson> DeleteTheoreticalLesson(long theoreticalLessonId)
        {
            var theoreticalLesson = await _context.TheoreticalLessons.FirstOrDefaultAsync(tl => tl.Id == theoreticalLessonId);

            if (theoreticalLesson != null)
            {
                var theoreticalLessonEvents = await _context.TheoreticalLessonEvents.Where(e => e.TheoreticalLessonId == theoreticalLessonId).ToListAsync();
                if (theoreticalLessonEvents != null)
                {
                    foreach (var e in theoreticalLessonEvents)
                    {
                        //delete each theoreticalLessonEvent
                    }
                    theoreticalLesson.TheoreticalLessonEvents.Clear();
                }
                _context.TheoreticalLessons.Remove(theoreticalLesson);
                await _context.SaveChangesAsync();
            }

            return theoreticalLesson;
        }
    }
}

using Calendaurus.Models.Model;
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

            _context.Disciplines.Add(discipline);
            await _context.SaveChangesAsync();

            return discipline;
        }

        public async Task<Discipline> UpdateDiscipline(long id, string name, string faculty, int year)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == id);

            if (discipline != null)
            {
                discipline.Name = name;
                discipline.Faculty = faculty;
                discipline.Year = (byte)year;

                await _context.SaveChangesAsync();
            }

            return discipline;
        }

        public async Task<PracticalLesson> CreatePracticalLesson(long disciplineId, string type, string description)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);

            if (discipline != null)
            {
                var practicalLesson = new PracticalLesson()
                {
                    Type = type,
                    Description = description
                };

                discipline.PracticalLessons.Add(practicalLesson);
                _context.PracticalLessons.Add(practicalLesson);
                await _context.SaveChangesAsync();
                return practicalLesson;
            }
            return null;
        }

        public async Task<PracticalLesson> UpdatePracticalLesson(long practicalLessonId, string type, string description)
        {
            var practicalLesson = await _context.PracticalLessons.FirstOrDefaultAsync(pl => pl.Id == practicalLessonId);

            if(practicalLesson != null) 
            {
                practicalLesson.Type = type;
                practicalLesson.Description = description;

                await _context.SaveChangesAsync();
            };

            return practicalLesson;
        }

        public async Task<TheoreticalLesson> CreateTheoreticalLesson(long disciplineId, string description)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);

            if (discipline != null)
            {
                var theoreticalLesson = new TheoreticalLesson()
                {
                    Description = description
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
    }
}

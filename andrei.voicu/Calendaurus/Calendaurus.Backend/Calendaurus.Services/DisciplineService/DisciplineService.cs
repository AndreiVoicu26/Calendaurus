using Calendaurus.Models.Model;
using Calendaurus.Services.PracticalLessonService;
using Calendaurus.Services.TheoreticalLessonService;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;


namespace Calendaurus.Services.DisciplineService
{
    public class DisciplineService : IDisciplineService
    {
        private readonly CalendaurusContext _context;

        private readonly IPracticalLessonService _practicalService;
        private readonly ITheoreticalLessonService _theoreticalService;

        public DisciplineService(CalendaurusContext context, IPracticalLessonService practicalService, ITheoreticalLessonService theoreticalService)
        {
            _context = context;
            _practicalService = practicalService;
            _theoreticalService = theoreticalService;
        }

        public async Task<List<Discipline>> GetDisciplines()
        {
            var disciplines = await _context.Disciplines.ToListAsync();
            return disciplines;
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

        public async Task<Discipline> UpdateDiscipline(long disciplineId, string name, string faculty, int year)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);

            if (discipline != null)
            {
                discipline.Name = name;
                discipline.Faculty = faculty;
                discipline.Year = (byte)year;

                await _context.SaveChangesAsync();
            }

            return discipline;
        }

        public async Task<Discipline> DeleteDiscipline(long disciplineId)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);

            if(discipline != null)
            {
                var practicalLessons = await _context.PracticalLessons.Where(pl => pl.DisciplineId == disciplineId).ToListAsync();
                if(practicalLessons != null)
                {
                    foreach (var pl in practicalLessons)
                    {
                        await _practicalService.DeletePracticalLesson(pl.Id);
                    }
                    discipline.PracticalLessons.Clear();
                }

                var theoreticalLesson = await _context.TheoreticalLessons.FirstOrDefaultAsync(tl => tl.DisciplineId == disciplineId);
                if(theoreticalLesson != null)
                {
                    await _theoreticalService.DeleteTheoreticalLesson(theoreticalLesson.Id);
                    discipline.TheoreticalLesson = null;
                }

                _context.Disciplines.Remove(discipline);
                await _context.SaveChangesAsync();
            }

            return discipline;
        }
    }
}

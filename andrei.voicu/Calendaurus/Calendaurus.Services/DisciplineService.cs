using Calendaurus.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

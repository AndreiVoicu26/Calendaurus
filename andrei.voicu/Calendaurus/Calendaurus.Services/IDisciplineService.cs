using Calendaurus.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services
{
    public interface IDisciplineService
    {
        Task<Discipline> CreateDiscipline(string name, string faculty, int year);
        Task<Discipline> UpdateDiscipline(long id, string name, string faculty, int year);
    }
}

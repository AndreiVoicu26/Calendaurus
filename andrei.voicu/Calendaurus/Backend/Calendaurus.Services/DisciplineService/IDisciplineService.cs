using Calendaurus.Models.Model;

namespace Calendaurus.Services.DisciplineService
{
    public interface IDisciplineService
    {
        Task<List<Discipline>> GetDisciplines();
        Task<Discipline> CreateDiscipline(string name, string faculty, int year);
        Task<Discipline> UpdateDiscipline(long disciplineId, string name, string faculty, int year);
        Task<Discipline> DeleteDiscipline(long disciplineId);
    }
}

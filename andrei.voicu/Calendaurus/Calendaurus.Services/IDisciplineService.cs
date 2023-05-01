using Calendaurus.Models.Model;

namespace Calendaurus.Services
{
    public interface IDisciplineService
    {
        Task<Discipline> CreateDiscipline(string name, string faculty, int year);
        Task<Discipline> UpdateDiscipline(long id, string name, string faculty, int year);
        Task<PracticalLesson> CreatePracticalLesson(long disciplineId, string type, string description);
        Task<PracticalLesson> UpdatePracticalLesson(long practicalLessonId, string type, string description);
        Task<TheoreticalLesson> CreateTheoreticalLesson(long disciplineOd, string description);
        Task<TheoreticalLesson> UpdateTheoreticalLesson(long theoreticalLessonId, string description);
    }
}

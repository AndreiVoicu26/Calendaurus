using Calendaurus.Models.Models;

namespace Calendaurus.Services
{
    public interface IDisciplineService
    {
        Task<Discipline> CreateDiscipline(string name, string faculty, int year);
        Task<Discipline?> UpdateDiscipline(long id, string name, string faculty, int year);
        Task<bool> EnrollStudentToPracticalLessonEvent(long studentId, long practicalLessonEventId);
        Task<bool> RemoveStudentEnrollment(long studentId, long enrollmentId);
    }
}

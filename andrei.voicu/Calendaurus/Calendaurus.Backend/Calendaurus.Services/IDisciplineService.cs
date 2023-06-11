using Calendaurus.Models.Models;

namespace Calendaurus.Services
{
    public interface IDisciplineService
    {
        Task<Discipline> CreateDiscipline(string name, string faculty, int year);
        Task<Discipline?> UpdateDiscipline(long id, string name, string faculty, int year);
        Task<bool> EnrollStudentToPracticalLessonEventAsync(Student student, long practicalLessonEventId);
        Task<bool> RemoveStudentEnrollment(Student student, long enrollmentId);
    }
}

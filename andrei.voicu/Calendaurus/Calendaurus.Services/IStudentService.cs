using Calendaurus.Models.Model;

namespace Calendaurus.Services
{
    public interface IStudentService
    {
        Task<bool> EnrollStudentToPracticalLessonEvent(long studentId, long practicalLessonId, long practicalLessonEventId);
        Task<bool> RemoveStudentPracticalEnrollment(long studentId, long practicalLessonId, long practicalLessonEventId);
        Task<bool> EnrollStudentToTheoreticalLessonEvent(long studentId, long theoreticalLessonId, long theoreticalLessonEventId);
        Task<bool> RemoveStudentTheoreticalEnrollment(long studentId, long theoreticalLessonId, long theoreticalLessonEventId);
    }
}

using Calendaurus.Models.Model;

namespace Calendaurus.Services.Student
{
    public interface IStudentService
    {
        Task<List<Discipline>> GetStudentDisciplines(int year);
        Task<List<PracticalLesson>> GetStudentPracticalLessons(int year);
        Task<List<PracticalLesson>> GetStudentPracticalLessonsForDiscipline(int year, long disciplineId);
        Task<List<PracticalLessonEvent>> GetStudentPracticalLessonsEvents(int year);
        Task<List<PracticalLessonEvent>> GetStudentPracticalLessonsEventsForPracticalLesson(int year, long practicalLessonId);
        Task<List<PracticalLessonEvent>> GetStudentPracticalLessonEnrollments(User user);
        Task<bool> EnrollStudentToPracticalLessonEvent(long studentId, long practicalLessonId, long practicalLessonEventId);
        Task<bool> RemoveStudentPracticalEnrollment(long studentId, long practicalLessonId, long practicalLessonEventId);
        Task<TheoreticalLesson> GetStudentTheoreticalLessonForDiscipline(int year, long disciplineId);
        Task<List<TheoreticalLessonEvent>> GetStudentTheoreticalLessonEventsForTheoreticalLesson(int year, long practicalLessonId);
        Task<List<TheoreticalLessonEvent>> GetStudentTheoreticalLessonEnrollments(User user);
        Task<bool> EnrollStudentToTheoreticalLessonEvent(long studentId, long theoreticalLessonId, long theoreticalLessonEventId);
        Task<bool> RemoveStudentTheoreticalEnrollment(long studentId, long theoreticalLessonId, long theoreticalLessonEventId);
    }
}

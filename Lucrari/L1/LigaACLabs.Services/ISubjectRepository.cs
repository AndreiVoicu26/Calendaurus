using LigaACLabs.Models.Entities;

namespace LigaACLabs.Services
{
    public interface ISubjectRepository
    {
        IEnumerable<Lab> GetLabOptions(Guid subjectId);
        IEnumerable<Subject> GetSubjectsForUser(Guid userId);
    }
}

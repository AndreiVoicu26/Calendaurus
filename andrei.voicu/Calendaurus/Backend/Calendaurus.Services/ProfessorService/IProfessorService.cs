using Calendaurus.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services.ProfessorService
{
    public interface IProfessorService
    {
        Task<List<PracticalLessonEvent>> GetProfessorPracticalEvents(long professorId);
        Task<PracticalLessonEvent> CreatePracticalLessonEvent
            (long practicalLessonId, long professorId, string day, TimeSpan startTime, TimeSpan endTime, int occurance, int size);
        Task<PracticalLessonEvent> UpdatePracticalLessonEvent
            (long practicalLessonEventId, long practicalLessonId, long professorId, string day, TimeSpan startTime, TimeSpan endTime, int occurance, int size);
        Task<PracticalLessonEvent> DeletePracticalLessonEvent(long practicalLessonEventId);
        Task<List<TheoreticalLessonEvent>> GetProfessorTheoreticalEvents(long professorId);
    }
}

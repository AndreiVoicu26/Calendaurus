using Calendaurus.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services
{
    public interface IProfessorService
    {
        Task<PracticalLessonEvent> CreatePracticalLessonEvent
            (long practicalLessonId, long professorId, string day, TimeSpan startTime, TimeSpan endTime, int occurance, int size);
    }
}

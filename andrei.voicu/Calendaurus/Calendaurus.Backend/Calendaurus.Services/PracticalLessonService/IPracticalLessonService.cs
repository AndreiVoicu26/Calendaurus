using Calendaurus.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services.PracticalLessonService
{
    public interface IPracticalLessonService
    {
        Task<List<PracticalLesson>> GetPracticalLessonsForDiscipline(long disicpkineId);
        Task<PracticalLesson> CreatePracticalLesson(long disciplineId, string type, string description);
        Task<PracticalLesson> UpdatePracticalLesson(long practicalLessonId, string type, string description);
        Task<PracticalLesson> DeletePracticalLesson(long practicalLessonId);
    }
}

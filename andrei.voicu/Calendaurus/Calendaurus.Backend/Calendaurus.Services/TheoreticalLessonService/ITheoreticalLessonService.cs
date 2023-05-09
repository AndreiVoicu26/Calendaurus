using Calendaurus.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendaurus.Services.TheoreticalLessonService
{
    public interface ITheoreticalLessonService
    {
        Task<List<TheoreticalLesson>> GetAllTheoreticalLessons();
        Task<TheoreticalLesson> CreateTheoreticalLesson(long disciplineId, string description);
        Task<TheoreticalLesson> UpdateTheoreticalLesson(long theoreticalLessonId, string description);
        Task<TheoreticalLesson> DeleteTheoreticalLesson(long theoreticalLessonId);
    }
}

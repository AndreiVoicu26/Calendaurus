using AutoMapper;
using Calendaurus.Models.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Calendaurus.API.Models
{
    public class CalendaurusProfile : Profile
    {
        public CalendaurusProfile()
        {
            CreateMap<Discipline, DisciplineApiModel>();
            CreateMap<PracticalLesson, PracticalLessonApiModel>();
            CreateMap<PracticalLessonEvent, PracticalLessonEventApiModel>();
        }
    }
}
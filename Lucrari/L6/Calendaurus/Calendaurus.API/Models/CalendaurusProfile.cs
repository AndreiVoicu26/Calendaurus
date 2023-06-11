using AutoMapper;
using Calendaurus.Models.Models;

namespace Calendaurus.API.Models
{
    public class CalendaurusProfile : Profile
    {
        public CalendaurusProfile()
        {
            CreateMap<Discipline, DisciplineApiModel>();
            CreateMap<PracticalLesson, PracticalLessonApiModel>();
            CreateMap<PracticalLessonEvent, PracticalLessonEventApiModel>();
            CreateMap<TheoreticalLesson, TheoreticalLessonApiModel>();
            CreateMap<TheoreticalLessonEvent, TheoreticalLessonEventApiModel>();
        }
    }
}

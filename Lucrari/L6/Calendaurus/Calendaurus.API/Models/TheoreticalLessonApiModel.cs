namespace Calendaurus.API.Models
{
    public class TheoreticalLessonApiModel
    {
        public long Id { get; set; }

        public long DisciplineId { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<TheoreticalLessonEventApiModel> TheoreticalLessonEvents { get; set; } = new List<TheoreticalLessonEventApiModel>();
    }
}

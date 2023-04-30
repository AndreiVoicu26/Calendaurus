namespace Calendaurus.API.Models
{
    public class PracticalLessonApiModel
    {
        public long Id { get; set; }

        public long DisciplineId { get; set; }

        public byte Type { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<PracticalLessonEventApiModel> PracticalLessonEvents { get; set; } = new List<PracticalLessonEventApiModel>();
    }
}

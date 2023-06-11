namespace Calendaurus.API.Models
{
    public class TheoreticalLessonEventApiModel
    {
        public long Id { get; set; }

        public long PracticalLessonId { get; set; }

        public string DayOfWeek { get; set; } = null!;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public byte? Occurance { get; set; }

        public byte? MaximumSize { get; set; }
    }
}
namespace Calendaurus.API.Requests
{
    public class UpdatePracticalLessonEventRequest
    {
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Occurance { get; set; }
        public int MaximumSize { get; set; }
    }
}

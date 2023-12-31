﻿namespace Calendaurus.API.Requests
{
    public class CreatePracticalLessonEventRequest
    {
        public string? DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set;}
        public TimeSpan EndTime { get; set;}
        public int Occurance { get; set;}
        public int MaximumSize { get; set;}
    }
}

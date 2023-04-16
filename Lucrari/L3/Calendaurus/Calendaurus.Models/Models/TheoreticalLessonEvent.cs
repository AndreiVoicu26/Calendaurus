using System;
using System.Collections.Generic;

namespace Calendaurus.Models.Models;

public partial class TheoreticalLessonEvent
{
    public long Id { get; set; }

    public long TheoreticalLessonId { get; set; }

    public long ProfessorId { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// An enum which describes the occurance. ex:1-weekly/2-bi-weekly
    /// </summary>
    public byte? Occurance { get; set; }

    public byte? MaximumSize { get; set; }

    public virtual TheoreticalLesson TheoreticalLesson { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

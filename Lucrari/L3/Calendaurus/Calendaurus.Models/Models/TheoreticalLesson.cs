using System;
using System.Collections.Generic;

namespace Calendaurus.Models.Models;

public partial class TheoreticalLesson
{
    public long Id { get; set; }

    public long DisciplineId { get; set; }

    /// <summary>
    /// A short description on what the students will be learning at this theoretical lesson
    /// </summary>
    public string? Description { get; set; }

    public virtual Discipline Discipline { get; set; } = null!;

    public virtual ICollection<TheoreticalLessonEvent> TheoreticalLessonEvents { get; set; } = new List<TheoreticalLessonEvent>();
}

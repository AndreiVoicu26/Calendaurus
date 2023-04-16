using System;
using System.Collections.Generic;

namespace Calendaurus.Models.Models;

public partial class PracticalLesson
{
    public long Id { get; set; }

    public long DisciplineId { get; set; }

    public byte Type { get; set; }

    /// <summary>
    /// A short description on what the students will be doing at this practical lesson
    /// </summary>
    public string? Description { get; set; }

    public virtual Discipline Discipline { get; set; } = null!;

    public virtual ICollection<PracticalLessonEvent> PracticalLessonEvents { get; set; } = new List<PracticalLessonEvent>();
}

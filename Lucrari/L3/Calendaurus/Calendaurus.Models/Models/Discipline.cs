using System;
using System.Collections.Generic;

namespace Calendaurus.Models.Models;

public partial class Discipline
{
    public long Id { get; set; }

    public byte Year { get; set; }

    public string Faculty { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<PracticalLesson> PracticalLessons { get; set; } = new List<PracticalLesson>();

    public virtual TheoreticalLesson? TheoreticalLesson { get; set; }
}

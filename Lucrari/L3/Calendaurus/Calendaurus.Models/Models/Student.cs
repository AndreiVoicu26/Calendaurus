using System;
using System.Collections.Generic;

namespace Calendaurus.Models.Models;

public partial class Student
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public byte Year { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<PracticalLessonEvent> PracticalLessonEvens { get; set; } = new List<PracticalLessonEvent>();

    public virtual ICollection<TheoreticalLessonEvent> TheoreticalLessonEvens { get; set; } = new List<TheoreticalLessonEvent>();
}

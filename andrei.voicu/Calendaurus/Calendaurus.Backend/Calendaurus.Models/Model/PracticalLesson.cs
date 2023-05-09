using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Model;

[Table("PracticalLesson")]
[Index("DisciplineId", "Type", Name = "UK_PracticalLesson_DisciplineIdType", IsUnique = true)]
public partial class PracticalLesson
{
    [Key]
    public long Id { get; set; }

    public long DisciplineId { get; set; }

    public string Type { get; set; } = null!;

    /// <summary>
    /// A short description on what the students will be doing at this practical lesson
    /// </summary>
    public string Description { get; set; }

    [ForeignKey("DisciplineId")]
    [InverseProperty("PracticalLessons")]
    public virtual Discipline Discipline { get; set; } = null!;

    [InverseProperty("PracticalLesson")]
    public virtual ICollection<PracticalLessonEvent> PracticalLessonEvents { get; set; } = new List<PracticalLessonEvent>();
}

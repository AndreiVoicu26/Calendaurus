using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Model;

[Table("TheoreticalLesson")]
[Index("DisciplineId", Name = "UK_TheoreticalLesson_DisciplineId", IsUnique = true)]
public partial class TheoreticalLesson
{
    [Key]
    public long Id { get; set; }

    public long DisciplineId { get; set; }

    /// <summary>
    /// A short description on what the students will be learning at this theoretical lesson
    /// </summary>
    public string? Description { get; set; }

    [ForeignKey("DisciplineId")]
    [InverseProperty("TheoreticalLesson")]
    public virtual Discipline Discipline { get; set; } = null!;

    [InverseProperty("TheoreticalLesson")]
    public virtual ICollection<TheoreticalLessonEvent> TheoreticalLessonEvents { get; set; } = new List<TheoreticalLessonEvent>();
}

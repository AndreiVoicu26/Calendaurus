using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Model;

[Table("TheoreticalLessonEvent")]
public partial class TheoreticalLessonEvent
{
    [Key]
    public long Id { get; set; }

    public long TheoreticalLessonId { get; set; }

    public long ProfessorId { get; set; }

    [StringLength(20)]
    public string DayOfWeek { get; set; } = null!;

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// An enum which describes the occurance. ex:1-weekly/2-bi-weekly
    /// </summary>
    public byte? Occurance { get; set; }

    public byte? MaximumSize { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("TheoreticalLessonEvents")]
    public virtual Professor Professor { get; set; } = null!;

    [ForeignKey("TheoreticalLessonId")]
    [InverseProperty("TheoreticalLessonEvents")]
    public virtual TheoreticalLesson TheoreticalLesson { get; set; } = null!;

    [ForeignKey("TheoreticalLessonEventId")]
    [InverseProperty("TheoreticalLessonEvents")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

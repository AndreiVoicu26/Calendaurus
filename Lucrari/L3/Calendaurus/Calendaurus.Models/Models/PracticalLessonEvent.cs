using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Models;

[Table("PracticalLessonEvent")]
public partial class PracticalLessonEvent
{
    [Key]
    public long Id { get; set; }

    public long PracticalLessonId { get; set; }

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

    [ForeignKey("PracticalLessonId")]
    [InverseProperty("PracticalLessonEvents")]
    public virtual PracticalLesson PracticalLesson { get; set; } = null!;

    [ForeignKey("PracticalLessonEvenId")]
    [InverseProperty("PracticalLessonEvens")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

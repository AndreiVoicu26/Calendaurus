using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Models;

[Table("Student")]
[Index("UserId", Name = "UK_Student_UserId", IsUnique = true)]
public partial class Student
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    public byte Year { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Student")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("Students")]
    public virtual ICollection<PracticalLessonEvent> PracticalLessonEvens { get; set; } = new List<PracticalLessonEvent>();

    [ForeignKey("StudentId")]
    [InverseProperty("Students")]
    public virtual ICollection<TheoreticalLessonEvent> TheoreticalLessonEvens { get; set; } = new List<TheoreticalLessonEvent>();
}

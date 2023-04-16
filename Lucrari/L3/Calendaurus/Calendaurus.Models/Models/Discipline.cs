using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Models;

[Table("Discipline")]
public partial class Discipline
{
    [Key]
    public long Id { get; set; }

    public byte Year { get; set; }

    [StringLength(200)]
    public string Faculty { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Discipline")]
    public virtual ICollection<PracticalLesson> PracticalLessons { get; set; } = new List<PracticalLesson>();

    [InverseProperty("Discipline")]
    public virtual TheoreticalLesson? TheoreticalLesson { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Models;

[Table("Professor")]
[Index("UserId", Name = "UK_Professor_UserId", IsUnique = true)]
public partial class Professor
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Professor")]
    public virtual User User { get; set; } = null!;
}

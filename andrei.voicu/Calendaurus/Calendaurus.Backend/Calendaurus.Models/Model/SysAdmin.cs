using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Model;

[Table("SysAdmin")]
[Index("UserId", Name = "UK_SysAdmin_UserId", IsUnique = true)]
public partial class SysAdmin
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("SysAdmin")]
    public virtual User User { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Models;

[Table("User")]
[Index("Email", Name = "UK_User_Email", IsUnique = true)]
public partial class User
{
    [Key]
    public long Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(29)]
    [Unicode(false)]
    public string? TelephoneNumber { get; set; }

    [InverseProperty("User")]
    public virtual Professor? Professor { get; set; }

    [InverseProperty("User")]
    public virtual Student? Student { get; set; }

    [InverseProperty("User")]
    public virtual SysAdmin? SysAdmin { get; set; }
}

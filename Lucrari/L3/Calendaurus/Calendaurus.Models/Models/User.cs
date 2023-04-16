using System;
using System.Collections.Generic;

namespace Calendaurus.Models.Models;

public partial class User
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? TelephoneNumber { get; set; }

    public virtual Professor? Professor { get; set; }

    public virtual Student? Student { get; set; }

    public virtual SysAdmin? SysAdmin { get; set; }
}

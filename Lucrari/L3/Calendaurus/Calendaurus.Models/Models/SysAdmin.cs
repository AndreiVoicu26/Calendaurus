using System;
using System.Collections.Generic;

namespace Calendaurus.Models.Models;

public partial class SysAdmin
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}

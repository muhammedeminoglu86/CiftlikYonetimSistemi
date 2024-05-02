using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class LogType
{
    public int Id { get; set; }

    public string? Logtypename { get; set; }

    public string? Logtypedescription { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<GeneralLog> GeneralLogs { get; set; } = new List<GeneralLog>();
}

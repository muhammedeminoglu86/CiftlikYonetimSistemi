using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalCount
{
    public int Id { get; set; }

    public int? Groupid { get; set; }

    public int? Count { get; set; }

    public DateTime? Creationdate { get; set; }

    public int? Isactive { get; set; }

    public virtual Group? Group { get; set; }
}

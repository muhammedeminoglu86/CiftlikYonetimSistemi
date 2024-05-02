using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalSupplementMapping
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Supplementid { get; set; }

    public DateTime? Creationdate { get; set; }

    public int? Isactive { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual Supplement? Supplement { get; set; }
}

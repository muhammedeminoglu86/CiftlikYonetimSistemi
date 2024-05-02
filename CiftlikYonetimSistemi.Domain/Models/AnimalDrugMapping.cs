using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalDrugMapping
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Drugid { get; set; }

    public DateTime? Creationdate { get; set; }

    public int? Isactive { get; set; }

    public int Disaseid { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual Disase Disase { get; set; } = null!;

    public virtual Drug? Drug { get; set; }
}

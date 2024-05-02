using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalInjureMapping
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Injureid { get; set; }

    public int? Isactive { get; set; }

    public DateTime? Creationdate { get; set; }

    public int? Isfinished { get; set; }

    public virtual Injure? Injure { get; set; }
}

using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Disase
{
    public int Id { get; set; }

    public string? Disasename { get; set; }

    public string? Disasedescription { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<AnimalDisaseMapping> AnimalDisaseMappings { get; set; } = new List<AnimalDisaseMapping>();

    public virtual ICollection<AnimalDrugMapping> AnimalDrugMappings { get; set; } = new List<AnimalDrugMapping>();
}

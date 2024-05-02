using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Drug
{
    public int Id { get; set; }

    public string? Drugname { get; set; }

    public string? Drugdescription { get; set; }

    public byte[]? Logo { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<AnimalDrugMapping> AnimalDrugMappings { get; set; } = new List<AnimalDrugMapping>();
}

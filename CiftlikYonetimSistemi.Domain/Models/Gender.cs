using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Gender
{
    public int Id { get; set; }

    public string? Gendername { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<GenderAnimalMapping> GenderAnimalMappings { get; set; } = new List<GenderAnimalMapping>();
}

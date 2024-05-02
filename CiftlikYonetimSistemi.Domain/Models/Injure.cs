using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Injure
{
    public int Id { get; set; }

    public string? Injurename { get; set; }

    public string? Insuredescription { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<AnimalInjureMapping> AnimalInjureMappings { get; set; } = new List<AnimalInjureMapping>();
}

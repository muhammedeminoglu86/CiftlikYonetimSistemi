using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class CongenitalCondition
{
    public int Id { get; set; }

    public string? Congenitalconditionname { get; set; }

    public string? Congenitalconditiondescription { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<AnimalCongenitalConditionMapping> AnimalCongenitalConditionMappings { get; set; } = new List<AnimalCongenitalConditionMapping>();
}

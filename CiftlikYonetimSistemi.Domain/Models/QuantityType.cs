using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class QuantityType
{
    public int Id { get; set; }

    public string? Quantitytypename { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<AttributeType> AttributeTypes { get; set; } = new List<AttributeType>();
}

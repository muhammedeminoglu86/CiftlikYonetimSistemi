using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AttributeType
{
    public int Id { get; set; }

    public string? Attributetypename { get; set; }

    public string? Attributetypedescription { get; set; }

    public int? Isunique { get; set; }

    public int? Isactive { get; set; }

    public int? Quantitytypeid { get; set; }

    public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();

    public virtual QuantityType? Quantitytype { get; set; }
}

using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AttributeValue
{
    public int Id { get; set; }

    public int? Attributetypeid { get; set; }

    public string? Attributevalue { get; set; }

    public int? Companyusermappingid { get; set; }

    public int? Isactive { get; set; }

    public DateTime? Creationdate { get; set; }

    public virtual AttributeType? Attributetype { get; set; }

    public virtual CompanyUserMapping? Companyusermapping { get; set; }
}

using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Companyname { get; set; }

    public string? Companydescription { get; set; }

    public byte[]? Companylogo { get; set; }

    public string? Phone1 { get; set; }

    public string? Phone2 { get; set; }

    public string? Manager1 { get; set; }

    public string? Manager2 { get; set; }

    public string? Address { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<CompanyUserMapping> CompanyUserMappings { get; set; } = new List<CompanyUserMapping>();
}

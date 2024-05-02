using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Group
{
    public int Id { get; set; }

    public string? Groupname { get; set; }

    public string? Groupdescription { get; set; }

    public int? Isactive { get; set; }

    public int? Companyusermappingid { get; set; }

    public virtual ICollection<AnimalCount> AnimalCounts { get; set; } = new List<AnimalCount>();

    public virtual ICollection<AnimalGroupMapping> AnimalGroupMappings { get; set; } = new List<AnimalGroupMapping>();
}

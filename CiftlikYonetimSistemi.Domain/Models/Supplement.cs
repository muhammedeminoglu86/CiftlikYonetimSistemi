using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Supplement
{
    public int Id { get; set; }

    public string? Supplementname { get; set; }

    public string? Supplementdescription { get; set; }

    public byte[]? Blob { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<AnimalSupplementMapping> AnimalSupplementMappings { get; set; } = new List<AnimalSupplementMapping>();
}

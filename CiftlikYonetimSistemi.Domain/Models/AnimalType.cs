using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalType
{
    public int Id { get; set; }

    public string? Animaltype { get; set; }

    public string? Typedesc { get; set; }

    public byte[]? Logo { get; set; }

    public int? Isactive { get; set; }

    public int? Userid { get; set; }

    public virtual ICollection<AnimalSubType> AnimalSubTypes { get; set; } = new List<AnimalSubType>();
}

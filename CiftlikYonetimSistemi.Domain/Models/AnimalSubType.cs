using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalSubType
{
    public int Id { get; set; }

    public int? Animaltypeid { get; set; }

    public string? Animalsubtypename { get; set; }

    public byte[]? Logo { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<AnimalSubTypeVaccineSchedule> AnimalSubTypeVaccineSchedules { get; set; } = new List<AnimalSubTypeVaccineSchedule>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual AnimalType? Animaltype { get; set; }
}

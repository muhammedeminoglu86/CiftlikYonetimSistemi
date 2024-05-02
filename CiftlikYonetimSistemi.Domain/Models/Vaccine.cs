using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Vaccine
{
    public int Id { get; set; }

    public string? Vaccinename { get; set; }

    public int? Text { get; set; }

    public byte[]? Logo { get; set; }

    public int? Isactive { get; set; }

    public virtual ICollection<AnimalSubTypeVaccineSchedule> AnimalSubTypeVaccineSchedules { get; set; } = new List<AnimalSubTypeVaccineSchedule>();

    public virtual ICollection<AnimalVaccineMapping> AnimalVaccineMappings { get; set; } = new List<AnimalVaccineMapping>();

    public virtual ICollection<AnimalVaccineSchedule> AnimalVaccineSchedules { get; set; } = new List<AnimalVaccineSchedule>();
}

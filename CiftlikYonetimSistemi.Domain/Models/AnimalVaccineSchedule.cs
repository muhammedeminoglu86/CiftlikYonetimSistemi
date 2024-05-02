using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalVaccineSchedule
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Vaccineid { get; set; }

    public DateTime? Planneddate { get; set; }

    public int? Creationdate { get; set; }

    public int? Isactive { get; set; }

    public int? Isdone { get; set; }

    public DateTime? Vaccinationdate { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}

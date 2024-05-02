using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalVaccineMapping
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Vaccineid { get; set; }

    public DateTime? Creationdate { get; set; }

    public int? Isactive { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}

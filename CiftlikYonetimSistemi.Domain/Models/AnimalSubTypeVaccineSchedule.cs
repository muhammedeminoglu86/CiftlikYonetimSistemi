using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalSubTypeVaccineSchedule
{
    public int Id { get; set; }

    public int? Animalsubtypeid { get; set; }

    public int? Vaccineid { get; set; }

    public DateTime? Creationdate { get; set; }

    public DateTime? Planneddate { get; set; }

    public int? Isdone { get; set; }

    public int? Companyusermapid { get; set; }

    public int? Isactive { get; set; }

    public virtual AnimalSubType? Animalsubtype { get; set; }

    public virtual CompanyUserMapping? Companyusermap { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}

using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Animal
{
    public int Id { get; set; }

    public string? Animalname { get; set; }

    public string? Animaldescription { get; set; }

    public string? Uniqueguid { get; set; }

    public int? Companyusermappingid { get; set; }

    public int? Isactive { get; set; }

    public DateTime? Creationdate { get; set; }

    public int? Parentanimalid { get; set; }

    public int? Animalsubtypeid { get; set; }

    public DateTime? Birthdate { get; set; }

    public virtual ICollection<AnimalCongenitalConditionMapping> AnimalCongenitalConditionMappings { get; set; } = new List<AnimalCongenitalConditionMapping>();

    public virtual ICollection<AnimalDisaseMapping> AnimalDisaseMappings { get; set; } = new List<AnimalDisaseMapping>();

    public virtual ICollection<AnimalDrugMapping> AnimalDrugMappings { get; set; } = new List<AnimalDrugMapping>();

    public virtual ICollection<AnimalGroupMapping> AnimalGroupMappings { get; set; } = new List<AnimalGroupMapping>();

    public virtual ICollection<AnimalRfid> AnimalRfids { get; set; } = new List<AnimalRfid>();

    public virtual ICollection<AnimalSupplementMapping> AnimalSupplementMappings { get; set; } = new List<AnimalSupplementMapping>();

    public virtual ICollection<AnimalVaccineMapping> AnimalVaccineMappings { get; set; } = new List<AnimalVaccineMapping>();

    public virtual ICollection<AnimalVaccineSchedule> AnimalVaccineSchedules { get; set; } = new List<AnimalVaccineSchedule>();

    public virtual AnimalSubType? Animalsubtype { get; set; }

    public virtual CompanyUserMapping? Companyusermapping { get; set; }

    public virtual GenderAnimalMapping? GenderAnimalMapping { get; set; }

    public virtual ICollection<Animal> InverseParentanimal { get; set; } = new List<Animal>();

    public virtual Animal? Parentanimal { get; set; }

    public virtual ICollection<Weaning> Weanings { get; set; } = new List<Weaning>();

    public virtual ICollection<Weight> Weights { get; set; } = new List<Weight>();
}

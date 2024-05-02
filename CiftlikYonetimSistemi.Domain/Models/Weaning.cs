using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Weaning
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public DateTime? Weaningdate { get; set; }

    public int? Isactive { get; set; }

    public int? Companyusermappingid { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual CompanyUserMapping? Companyusermapping { get; set; }
}

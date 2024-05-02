using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class Weight
{
    public int Id { get; set; }

    public decimal? Weightx { get; set; }

    public int? Animalid { get; set; }

    public int? Creationdate { get; set; }

    public virtual Animal? Animal { get; set; }
}

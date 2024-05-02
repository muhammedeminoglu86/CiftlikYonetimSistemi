using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalRfid
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public string? Rfidcode { get; set; }

    public int? Isactive { get; set; }

    public DateTime? Creationdate { get; set; }

    public virtual Animal? Animal { get; set; }
}

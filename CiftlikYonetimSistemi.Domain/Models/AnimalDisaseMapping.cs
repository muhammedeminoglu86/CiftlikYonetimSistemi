using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalDisaseMapping
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Disaseid { get; set; }

    public DateTime? Creationdate { get; set; }

    public int? Isfinished { get; set; }

    public DateTime? Finishtime { get; set; }

    public int? Isactive { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual Disase? Disase { get; set; }
}

using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalGroupMapping
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Groupid { get; set; }

    public int? Isactive { get; set; }

    public DateTime? Creationdate { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual Group? Group { get; set; }
}

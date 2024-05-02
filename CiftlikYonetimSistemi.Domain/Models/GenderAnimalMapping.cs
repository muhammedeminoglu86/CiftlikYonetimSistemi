using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class GenderAnimalMapping
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Genderid { get; set; }

    public int? Isactive { get; set; }

    public DateTime? Creationdate { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual Animal IdNavigation { get; set; } = null!;
}

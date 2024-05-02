using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class AnimalCongenitalConditionMapping
{
    public int Id { get; set; }

    public int? Animalid { get; set; }

    public int? Congetialconditionid { get; set; }

    public int? Creationdate { get; set; }

    public int? Isfinished { get; set; }

    public DateTime? Finishtime { get; set; }

    public int? Isactive { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual CongenitalCondition? Congetialcondition { get; set; }
}

using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class CompanyUserMapping
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? Companyid { get; set; }

    public int? Assingedby { get; set; }

    public DateTime? Creationdate { get; set; }

    public string? Ipaddress { get; set; }

    public virtual ICollection<AnimalSubTypeVaccineSchedule> AnimalSubTypeVaccineSchedules { get; set; } = new List<AnimalSubTypeVaccineSchedule>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();

    public virtual Company? Company { get; set; }

    public virtual ICollection<GeneralLog> GeneralLogs { get; set; } = new List<GeneralLog>();

    public virtual User? User { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    public virtual ICollection<Weaning> Weanings { get; set; } = new List<Weaning>();
}

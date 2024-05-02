using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public int? Isactive { get; set; }
	public int? UserTypeId { get; set; }


	public virtual ICollection<CompanyUserMapping> CompanyUserMappings { get; set; } = new List<CompanyUserMapping>();
}

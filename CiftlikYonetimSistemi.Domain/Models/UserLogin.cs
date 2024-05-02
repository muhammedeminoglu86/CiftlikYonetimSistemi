using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class UserLogin
{
    public int Id { get; set; }

    public int? Companyusermappingid { get; set; }

    public string? Generatedtoken { get; set; }

    public DateTime? Sessiontimeout { get; set; }

    public DateTime? Logintime { get; set; }

    public int? Isloggedout { get; set; }

    public DateTime? Logouttime { get; set; }

    public int? Attemptid { get; set; }

    public virtual LoginAttempt? Attempt { get; set; }

    public virtual CompanyUserMapping? Companyusermapping { get; set; }
}

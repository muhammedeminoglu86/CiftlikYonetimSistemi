using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class GeneralLog
{
    public int Id { get; set; }

    public string? Controllername { get; set; }

    public string? Actionname { get; set; }

    public string? Description { get; set; }

    public int? Logtypeid { get; set; }

    public string? Exceptionmessage { get; set; }

    public int? Companyusermappingid { get; set; }

    public DateTime? Creationdate { get; set; }

    public virtual CompanyUserMapping? Companyusermapping { get; set; }

    public virtual LogType? Logtype { get; set; }
}

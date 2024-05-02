using System;
using System.Collections.Generic;

namespace CiftlikYonetimSistemi.Domain.Models;

public partial class SiteSetting
{
    public int Id { get; set; }

    public string? Sitetitle { get; set; }

    public string? Sitedescription { get; set; }

    public string? Sitekeyword { get; set; }

    public string? Siteurl { get; set; }

    public string? Controllername { get; set; }

    public string? Actionname { get; set; }

    public int? Sitemode { get; set; }

    public int? Isactive { get; set; }
}

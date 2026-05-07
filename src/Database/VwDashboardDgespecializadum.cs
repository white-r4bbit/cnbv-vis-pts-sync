using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwDashboardDgespecializadum
{
    public int IdAccion { get; set; }

    public string ClaveDge { get; set; } = null!;

    public string? NombreDge { get; set; }

    public DateTime? FechaRegistro { get; set; }
}

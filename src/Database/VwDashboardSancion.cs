using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwDashboardSancion
{
    public int IdAccion { get; set; }

    public string? Evento { get; set; }

    public DateTime? Fecha { get; set; }
}

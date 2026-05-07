using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwDashboardFactorRiesgo
{
    public int IdAccion { get; set; }

    public int? RetrasoInicio { get; set; }

    public int? RetrasoFin { get; set; }

    public int? RetrasoMedidas { get; set; }

    public int? RetrasoObs { get; set; }

    public int? FactorRiesgo { get; set; }
}

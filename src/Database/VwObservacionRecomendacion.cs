using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwObservacionRecomendacion
{
    public int IdObservacion { get; set; }

    public int IdAccion { get; set; }

    public DateTime? FechaNotifOficio { get; set; }

    public string? NumOficio { get; set; }

    public int? NumObsEmitidas { get; set; }

    public DateTime? FechaUltimaResp { get; set; }

    public DateTime? FechaContestacion { get; set; }

    public int? NumObsDesvirtuadas { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaLimiteOficio { get; set; }

    public DateTime? FechaLimiteResp { get; set; }
}

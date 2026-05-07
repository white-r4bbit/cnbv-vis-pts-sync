using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class EjecucionAccion
{
    public int IdEjecucion { get; set; }

    public int IdAccion { get; set; }

    public DateTime? FechaInicioReal { get; set; }

    public DateTime? FechaNotifOficio { get; set; }

    public string? NumOficioOrden { get; set; }

    public DateTime? FechaFinReal { get; set; }

    public DateTime? FechaInforme { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public bool Habilitado { get; set; }

    public bool Terminado { get; set; }

    public virtual AccionSupervision IdAccionNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class SolicitudCancelacion
{
    public int IdCancelacion { get; set; }

    public int IdAccion { get; set; }

    public string? NumMemorandum { get; set; }

    public string? Motivo { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public virtual AccionSupervision IdAccionNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class Sancion
{
    public int IdSancion { get; set; }

    public int IdAccion { get; set; }

    public string? IdSolicitudExterno { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public int IdEstatusSancion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public virtual SancionControl IdAccionNavigation { get; set; } = null!;

    public virtual EstatusSancion IdEstatusSancionNavigation { get; set; } = null!;
}

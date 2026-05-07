using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwSancion
{
    public int IdSancion { get; set; }

    public int IdAccion { get; set; }

    public string? IdSolicitudExterno { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public int IdEstatusSancion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public string? DescEstatus { get; set; }

    public string? ClaveEstatus { get; set; }
}

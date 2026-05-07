using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class BitacoraEstatus
{
    public int IdBitacora { get; set; }

    public int IdAccion { get; set; }

    public int? IdEstatusAnterior { get; set; }

    public int? IdEstatusNuevo { get; set; }

    public DateTime? FechaCambio { get; set; }

    public string? UsuarioCambio { get; set; }

    public string? Observacion { get; set; }

    public virtual AccionSupervision IdAccionNavigation { get; set; } = null!;

    public virtual EstatusAccion? IdEstatusAnteriorNavigation { get; set; }

    public virtual EstatusAccion? IdEstatusNuevoNavigation { get; set; }
}

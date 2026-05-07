using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class EstatusAccion
{
    public int IdEstatus { get; set; }

    public string? ClaveEstatus { get; set; }

    public string? DescEstatus { get; set; }

    public string? ColorSemaforo { get; set; }

    public virtual ICollection<BitacoraEstatus> BitacoraEstatusIdEstatusAnteriorNavigations { get; set; } = new List<BitacoraEstatus>();

    public virtual ICollection<BitacoraEstatus> BitacoraEstatusIdEstatusNuevoNavigations { get; set; } = new List<BitacoraEstatus>();
}

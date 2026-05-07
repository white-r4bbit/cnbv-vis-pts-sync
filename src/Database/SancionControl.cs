using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class SancionControl
{
    public int IdAccion { get; set; }

    public bool Habilitado { get; set; }

    public bool Terminado { get; set; }

    public bool? Omitido { get; set; }

    public string? JustificacionOmision { get; set; }

    public virtual AccionSupervision IdAccionNavigation { get; set; } = null!;

    public virtual ICollection<Sancion> Sancions { get; set; } = new List<Sancion>();
}

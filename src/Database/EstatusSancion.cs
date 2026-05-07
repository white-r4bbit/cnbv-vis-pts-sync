using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class EstatusSancion
{
    public int IdEstatusSancion { get; set; }

    public string? ClaveEstatus { get; set; }

    public string? DescEstatus { get; set; }

    public virtual ICollection<Sancion> Sancions { get; set; } = new List<Sancion>();
}

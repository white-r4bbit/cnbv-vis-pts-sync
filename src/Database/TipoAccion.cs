using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class TipoAccion
{
    public int IdTipo { get; set; }

    public string? ClaveSubtipo { get; set; }

    public string? DescSubtipo { get; set; }

    public bool? TipoCalculado { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<AccionSupervision> AccionSupervisions { get; set; } = new List<AccionSupervision>();
}

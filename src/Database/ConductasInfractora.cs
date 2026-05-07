using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class ConductasInfractora
{
    public int IdConducta { get; set; }

    public int IdAccion { get; set; }

    public int? NumConductas { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public virtual AccionSupervision IdAccionNavigation { get; set; } = null!;
}

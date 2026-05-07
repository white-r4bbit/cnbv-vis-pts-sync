using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class AccionMedidaCorrectiva
{
    public int IdAymc { get; set; }

    public int IdAccion { get; set; }

    public DateTime? FechaNotifOficio { get; set; }

    public string? NumOficio { get; set; }

    public int? NumAymc { get; set; }

    public DateTime? FechaRespEntidad { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public bool Habilitado { get; set; }

    public bool Terminado { get; set; }

    public bool? Omitido { get; set; }

    public string? JustificacionOmision { get; set; }

    public virtual AccionSupervision IdAccionNavigation { get; set; } = null!;
}

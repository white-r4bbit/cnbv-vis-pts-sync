using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwAccionMedidaCorrectiva
{
    public int IdAymc { get; set; }

    public int IdAccion { get; set; }

    public DateTime? FechaNotifOficio { get; set; }

    public string? NumOficio { get; set; }

    public int? NumAymc { get; set; }

    public DateTime? FechaRespEntidad { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaLimiteEmision { get; set; }
}

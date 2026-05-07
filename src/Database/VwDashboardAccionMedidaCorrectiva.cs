using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwDashboardAccionMedidaCorrectiva
{
    public int IdAccion { get; set; }

    public string? Evento { get; set; }

    public DateTime? Fecha { get; set; }

    public int IdAymc { get; set; }

    public string? NumOficio { get; set; }

    public int? NumAymc { get; set; }

    public string? UsuarioRegistro { get; set; }
}

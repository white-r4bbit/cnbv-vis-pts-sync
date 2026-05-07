using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwDashboardPresentacion
{
    public int? IdAccion { get; set; }

    public int? IdEjecucion { get; set; }

    public DateTime? FechaInicioPlan { get; set; }

    public string? DescSubtipo { get; set; }

    public string? ClaveSubtipo { get; set; }

    public string? DenominacionEntidad { get; set; }

    public DateTime? FechaFinPlan { get; set; }

    public DateTime? FechaInicioReal { get; set; }

    public DateTime? FechaFinReal { get; set; }

    public DateTime? FechaNotifOficio { get; set; }

    public string? NumOficioOrden { get; set; }

    public DateTime? FechaInforme { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? DiasRetrasoInicio { get; set; }

    public int? DiasRetrasoFin { get; set; }

    public string? SituacionDeInicio { get; set; }

    public string? SituacionDeConclusion { get; set; }

    public int? IdEstatus { get; set; }

    public string? DescEstatus { get; set; }

    public string? NombreSector { get; set; }

    public string? NombreSubsector { get; set; }

    public string? ClaveDg { get; set; }

    public string? NombreVp { get; set; }

    public string? ClaveVp { get; set; }
}

using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class VwAccionSupervision
{
    public int IdAccion { get; set; }

    public int? IdEntidadExt { get; set; }

    public string? Casfim { get; set; }

    public string? ClavePes { get; set; }

    public string? DenominacionEntidad { get; set; }

    public string? NombreCortoEntidad { get; set; }

    public int? IdSectorExt { get; set; }

    public string? NombreSector { get; set; }

    public string? NombreSubsector { get; set; }

    public int? IdSubsectorExt { get; set; }

    public int? IdVpExt { get; set; }

    public string? ClaveDg { get; set; }

    public int? IdDgExt { get; set; }

    public string? NombreVp { get; set; }

    public string? ClaveVp { get; set; }

    public string? CeferPeriodo { get; set; }

    public string? CeferRegistro { get; set; }

    public int IdTipoAccion { get; set; }

    public string? NombreDg { get; set; }

    public bool? ParticipaEspecializada { get; set; }

    public DateTime? FechaFinPlan { get; set; }

    public DateTime? FechaInicioPlan { get; set; }

    public string? CeferActualizada { get; set; }

    public string? UsuarioUltimaMod { get; set; }

    public string? UsuarioAlta { get; set; }

    public string? Comentarios { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? UltimaActualizacion { get; set; }
}

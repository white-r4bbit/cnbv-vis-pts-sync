using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class AccionSupervision
{
    public int? IdEntidadExt { get; set; }

    public string? ClavePes { get; set; }

    public string? Casfim { get; set; }

    public string? DenominacionEntidad { get; set; }

    public string? NombreCortoEntidad { get; set; }

    public int? IdSectorExt { get; set; }

    public string? NombreSector { get; set; }

    public int? IdSubsectorExt { get; set; }

    public string? NombreSubsector { get; set; }

    public int? IdVpExt { get; set; }

    public string? ClaveVp { get; set; }

    public string? NombreVp { get; set; }

    public int? IdDgExt { get; set; }

    public string? ClaveDg { get; set; }

    public string? NombreDg { get; set; }

    public int IdTipoAccion { get; set; }

    public string? CeferRegistro { get; set; }

    public string? CeferPeriodo { get; set; }

    public string? CeferActualizada { get; set; }

    public DateTime? FechaInicioPlan { get; set; }

    public DateTime? FechaFinPlan { get; set; }

    public bool? ParticipaEspecializada { get; set; }

    public string? Comentarios { get; set; }

    public DateTime? FechaAlta { get; set; }

    public string? UsuarioAlta { get; set; }

    public string? UsuarioUltimaMod { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public int IdAccion { get; set; }

    public bool Habilitado { get; set; }

    public bool Terminado { get; set; }

    public string? MotivoRechazo { get; set; }

    public int IdEstado { get; set; }

    public virtual ICollection<AccionDgespecializadum> AccionDgespecializada { get; set; } = new List<AccionDgespecializadum>();

    public virtual ICollection<AccionEstadoHistorial> AccionEstadoHistorials { get; set; } = new List<AccionEstadoHistorial>();

    public virtual AccionMedidaCorrectiva? AccionMedidaCorrectiva { get; set; }

    public virtual BitacoraEstatus? BitacoraEstatus { get; set; }

    public virtual ConductasInfractora? ConductasInfractora { get; set; }

    public virtual EjecucionAccion? EjecucionAccion { get; set; }

    public virtual EstadoFormulario IdEstadoNavigation { get; set; } = null!;

    public virtual TipoAccion IdTipoAccionNavigation { get; set; } = null!;

    public virtual ObservacionRecomendacion? ObservacionRecomendacion { get; set; }

    public virtual SancionControl? SancionControl { get; set; }

    public virtual SolicitudCancelacion? SolicitudCancelacion { get; set; }
}

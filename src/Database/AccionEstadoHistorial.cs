using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class AccionEstadoHistorial
{
    public int Id { get; set; }

    public int IdAccion { get; set; }

    public int IdEstado { get; set; }

    public DateTime RegistradoEl { get; set; }

    public string RegistradoPor { get; set; } = null!;

    public string? Comentario { get; set; }

    public virtual AccionSupervision IdAccionNavigation { get; set; } = null!;

    public virtual EstadoFormulario IdEstadoNavigation { get; set; } = null!;
}

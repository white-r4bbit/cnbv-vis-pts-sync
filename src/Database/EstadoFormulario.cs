using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class EstadoFormulario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EsFinal { get; set; }

    public bool TransicionAutomatica { get; set; }

    public bool RequiereComentario { get; set; }

    public virtual ICollection<AccionEstadoHistorial> AccionEstadoHistorials { get; set; } = new List<AccionEstadoHistorial>();

    public virtual ICollection<AccionSupervision> AccionSupervisions { get; set; } = new List<AccionSupervision>();
}

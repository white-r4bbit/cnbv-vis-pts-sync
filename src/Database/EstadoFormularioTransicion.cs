using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class EstadoFormularioTransicion
{
    public int Id { get; set; }

    public int IdEstadoOrigen { get; set; }

    public int IdEstadoDestino { get; set; }

    public bool Activo { get; set; }
}

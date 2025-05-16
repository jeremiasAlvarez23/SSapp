using System;
using System.Collections.Generic;

namespace SecuritysApp.Negocio.Entidades;

public partial class UsuarioSistema
{
    public int UsuarioId { get; set; }

    public int SistemaId { get; set; }

    public string RolSistema { get; set; } = null!;

    public bool TieneAcceso { get; set; }

    public virtual Sistema Sistema { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}

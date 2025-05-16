using System;
using System.Collections.Generic;

namespace SecuritysApp.Negocio.Entidades;

public partial class Rol
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}

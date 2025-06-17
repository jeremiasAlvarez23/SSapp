using System;
using System.Collections.Generic;

namespace SecuritysApp.Negocio.Entidades;

public partial class RolMenu
{
    public int Id { get; set; }

    public int? RolId { get; set; }

    public int? MenuId { get; set; }

    public virtual Menu? Menu { get; set; }

    public virtual Rol? Rol { get; set; }
}

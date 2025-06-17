using System;
using System.Collections.Generic;

namespace SecuritysApp.Negocio.Entidades;

public partial class Menu
{
    public int MenuId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Componente { get; set; }

    public string? Ruta { get; set; }

    public int MenuPadreId { get; set; }

    public int? Orden { get; set; }

    public string? Icono { get; set; }

    public string? Color { get; set; }

    public bool? Visible { get; set; }

    public bool? Activo { get; set; }

    public int SistemaId { get; set; }

    public virtual ICollection<RolMenu> RolMenu { get; set; } = new List<RolMenu>();

    public virtual Sistema Sistema { get; set; } = null!;
}

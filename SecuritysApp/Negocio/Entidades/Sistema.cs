using System;
using System.Collections.Generic;

namespace SecuritysApp.Negocio.Entidades;

public partial class Sistema
{
    public int SistemaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string UrlBase { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Menu> Menu { get; set; } = new List<Menu>();

    public virtual ICollection<UsuarioSistema> UsuarioSistema { get; set; } = new List<UsuarioSistema>();
}

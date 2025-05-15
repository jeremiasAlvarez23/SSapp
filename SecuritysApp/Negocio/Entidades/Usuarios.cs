using System;
using System.Collections.Generic;

namespace SecuritysApp.Negocio.Entidades;

public partial class Usuarios
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Activo { get; set; }
}

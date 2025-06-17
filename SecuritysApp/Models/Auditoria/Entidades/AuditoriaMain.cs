using System;
using System.Collections.Generic;

namespace SecuritysApp.Models.Auditoria.Entidades;

public partial class AuditoriaMain
{
    public int AuditoriaId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Email { get; set; }

    public string? Nombre { get; set; }

    public string? Sistema { get; set; }

    public string Accion { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime Fecha { get; set; }

    public string? Ip { get; set; }

    public string? Navegador { get; set; }

    public bool Exitoso { get; set; }
}

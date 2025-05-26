using Microsoft.EntityFrameworkCore;
using SecuritysApp.Data;
using SecuritysApp.Negocio.Extension;

namespace SecuritysApp.Negocio.Gestores
{
    public static class PerfilGestor
    {
        public static object? ObtenerPerfil(int usuarioId)
        {
            using var context = new SecuritysContext();
            var usuario = context.Usuario
                .Include(u => u.Rol)
                .Include(u => u.UsuarioSistema)
                .FirstOrDefault(u => u.UsuarioId == usuarioId);

            if (usuario == null) return null;

            return new
            {
                usuario.UsuarioId,
                usuario.Nombre,
                usuario.Email,
                Rol = usuario.Rol?.Nombre ?? "Usuario",
                Sistemas = usuario.UsuarioSistema
                    .Where(us => us.TieneAcceso)
                    .Select(us => us.SistemaId)
                    .ToList()
            };
        }
    }
}

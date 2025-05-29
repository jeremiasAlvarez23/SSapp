using Microsoft.EntityFrameworkCore;
using SecuritysApp.Data;
using SecuritysApp.Auditoria.Gestores;

namespace SecuritysApp.Negocio.Gestores
{
    public static class PerfilGestor
    {
        public static object? ObtenerPerfil(int usuarioEjecutorId, string? ip = null, string? userAgent = null)
        {
            using var context = new SecuritysContext();
            var usuario = context.Usuario
                .Include(u => u.Rol)
                .Include(u => u.UsuarioSistema)
                .FirstOrDefault(u => u.UsuarioId == usuarioEjecutorId);

            if (usuario == null)
            {
                AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Acceso Perfil Fallido", $"Intento de acceso a perfil no existente: {usuarioEjecutorId}", "Usuario", null, usuarioEjecutorId, ip, userAgent, "SecuritysApp", false);
                return null;
            }

            AuditoriaGestor.RegistrarEvento(usuario.UsuarioId, "Acceso Perfil", $"Usuario {usuario.Email} accedió a su perfil", "Usuario", usuario.UsuarioId.ToString(), usuario.UsuarioId, ip, userAgent, "SecuritysApp", true);

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

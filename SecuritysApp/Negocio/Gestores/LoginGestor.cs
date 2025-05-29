using Microsoft.EntityFrameworkCore;
using SecuritysApp.Data;
using SecuritysApp.Models.JwtService;
using SecuritysApp.Models.Login;
using SecuritysApp.Utils;
using SecuritysApp.Auditoria.Gestores;

namespace SecuritysApp.Negocio.Gestores
{
    public static class LoginGestor
    {
        public static LoginResponse? AutenticarUsuario(LoginRequest request, string? ip = null, string? userAgent = null)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario
                .Include(u => u.UsuarioSistema)
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Email == request.Email && u.Activo);

            if (usuario == null)
            {
                AuditoriaGestor.RegistrarEvento(0, "Login Fallido", $"Intento fallido para el email {request.Email}", "Usuario", null, null, ip, userAgent, "SecuritysApp", false);
                return null;
            }

            if (!PasswordHelper.Verificar(request.Clave, usuario.PasswordHash))
            {
                AuditoriaGestor.RegistrarEvento(usuario.UsuarioId, "Login Fallido", $"Intento fallido para el usuario {usuario.Email}", "Usuario", usuario.UsuarioId.ToString(), usuario.UsuarioId, ip, userAgent, "SecuritysApp", false);
                return null;
            }

            var sistemas = usuario.UsuarioSistema
                .Where(us => us.TieneAcceso)
                .Select(us => us.SistemaId)
                .ToList();

            var rol = usuario.Rol?.Nombre ?? "Usuario";
            var token = JwtService.GenerarToken(usuario.UsuarioId, usuario.Email, sistemas, rol);

            AuditoriaGestor.RegistrarEvento(usuario.UsuarioId, "Login Exitoso", $"Usuario {usuario.Email} inició sesión correctamente", "Usuario", usuario.UsuarioId.ToString(), usuario.UsuarioId, ip, userAgent, "SecuritysApp", true);

            return new LoginResponse
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                SistemasPermitidos = sistemas.Select(id => id.ToString()).ToList(),
                Token = token
            };
        }
    }
}

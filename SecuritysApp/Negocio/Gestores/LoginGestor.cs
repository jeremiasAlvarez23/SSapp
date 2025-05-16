using Microsoft.EntityFrameworkCore;
using SecuritysApp.Data;
using SecuritysApp.Models.JwtService;
using SecuritysApp.Models.Login;
using SecuritysApp.Utils;

namespace SecuritysApp.Negocio.Gestores
{
    public static class LoginGestor
    {
        public static LoginResponse? AutenticarUsuario(LoginRequest request)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario
                .Include(u => u.UsuarioSistema)
                .FirstOrDefault(u => u.Email == request.Email && u.Activo);

            if (usuario == null)
                return null;

            // TODO: Validar contraseña con hash
            if (!PasswordHelper.Verificar(request.Clave, usuario.PasswordHash))
                return null;


            var sistemas = usuario.UsuarioSistema
                .Where(us => us.TieneAcceso)
                .Select(us => us.SistemaId)
                .ToList();

            var token = JwtService.GenerarToken(usuario.UsuarioId, usuario.Email, sistemas);

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

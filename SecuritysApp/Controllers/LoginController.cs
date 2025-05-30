using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Models.Login;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Auditoria.Gestores;
using SecuritysApp.Routes;
using SecuritysApp.Utils;

namespace SecuritysApp.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class LoginController : AppController
    {
        [HttpPost(AppRoutes.v1.Login.Post)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();

            var resultado = LoginGestor.AutenticarUsuario(request, ip, userAgent);

            if (resultado == null)
                return Unauthorized(new { mensaje = "Credenciales inválidas" });

            return Ok(resultado);
        }

        [HttpPost(AppRoutes.v1.Login.Logout)]
        public IActionResult Logout()
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Logout", $"El usuario {usuarioEjecutorId} cerró sesión.", "Usuario", usuarioEjecutorId.ToString(), usuarioEjecutorId, ip, userAgent, "SecuritysApp", true);

            // No necesitas invalidar el token aquí (eso lo maneja el frontend)
            return Ok(new { mensaje = "Logout registrado" });
        }
    }
}

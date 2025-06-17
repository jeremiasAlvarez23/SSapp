using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Routes;
using SecuritysApp.Utils;

namespace SecuritysApp.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PerfilController : AppController
    {
        [HttpGet(AppRoutes.v1.Perfil.ObtenerPerfil)]
        public IActionResult ObtenerPerfil()
        {
            var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UsuarioId")?.Value;
            if (usuarioIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
                return Unauthorized();

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();

            var perfil = PerfilGestor.ObtenerPerfil(usuarioId, ip, userAgent);
            if (perfil == null)
                return NotFound();

            return Ok(perfil);
        }
    }
}

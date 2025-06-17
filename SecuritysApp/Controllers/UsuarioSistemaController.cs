using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Models.UsuarioSistema;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Routes;
using SecuritysApp.Utils;

namespace SecuritysApp.Controllers
{
    // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1")]
    public class UsuarioSistemaController : AppController
    {
        [HttpPost(AppRoutes.v1.UsuarioSistema.Insertar)]
        public IActionResult Insertar([FromBody] UsuarioSistemaRequest request)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            UsuarioSistemaGestor.Insertar(request, usuarioEjecutorId, ip, userAgent);
            return Ok("Relación Usuario-Sistema insertada.");
        }

        [HttpGet(AppRoutes.v1.UsuarioSistema.ObtenerTodo)]
        public IActionResult ObtenerTodo()
        {
            return Ok(UsuarioSistemaGestor.ObtenerTodo());
        }

        [HttpPut(AppRoutes.v1.UsuarioSistema.Editar)]
        public IActionResult Editar([FromBody] UsuarioSistemaRequest request)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            return UsuarioSistemaGestor.Editar(request, usuarioEjecutorId, ip, userAgent) ? Ok("Relación actualizada.") : NotFound();
        }

        [HttpDelete(AppRoutes.v1.UsuarioSistema.Eliminar)]
        public IActionResult Eliminar([FromQuery] int usuarioId, [FromQuery] int sistemaId)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            return UsuarioSistemaGestor.Eliminar(usuarioId, sistemaId, usuarioEjecutorId, ip, userAgent) ? Ok("Relación eliminada.") : NotFound();
        }
    }
}

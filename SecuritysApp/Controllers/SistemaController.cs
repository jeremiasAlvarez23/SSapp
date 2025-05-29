using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Models.Sistema;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Routes;
using SecuritysApp.Utils;

namespace SecuritysApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1")]
    public class SistemaController : AppController
    {
        [HttpPost(AppRoutes.v1.Sistema.Insertar)]
        public IActionResult Insertar(InsertarSistemaRequest request)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            SistemaGestor.Insertar(request, usuarioEjecutorId, ip, userAgent);
            return Ok("Sistema insertado correctamente.");
        }

        [HttpGet(AppRoutes.v1.Sistema.ObtenerTodo)]
        public IActionResult ObtenerTodo()
        {
            return Ok(SistemaGestor.ObtenerTodo());
        }

        [HttpGet(AppRoutes.v1.Sistema.ObtenerPorId)]
        public IActionResult ObtenerPorId(int id)
        {
            var sistema = SistemaGestor.ObtenerPorId(id);
            return sistema == null ? NotFound() : Ok(sistema);
        }

        [HttpGet(AppRoutes.v1.Sistema.ObtenerConPaginacion)]
        public IActionResult ObtenerConPaginacion([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? busqueda = null, [FromQuery] bool? activo = null)
        {
            var sistemas = SistemaGestor.ObtenerConPaginacion(skip, take, busqueda, activo);
            return Ok(sistemas);
        }

        [HttpPut(AppRoutes.v1.Sistema.Editar)]
        public IActionResult Editar(int id, InsertarSistemaRequest request)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            return SistemaGestor.Editar(id, request, usuarioEjecutorId, ip, userAgent) ? Ok("Sistema editado.") : NotFound();
        }

        [HttpPut(AppRoutes.v1.Sistema.Activar)]
        public IActionResult Activar(int id)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            return SistemaGestor.Activar(id, usuarioEjecutorId, ip, userAgent) ? Ok("Sistema activado.") : NotFound();
        }

        [HttpDelete(AppRoutes.v1.Sistema.Desactivar)]
        public IActionResult Desactivar(int id)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            return SistemaGestor.Desactivar(id, usuarioEjecutorId, ip, userAgent) ? Ok("Sistema desactivado.") : NotFound();
        }
    }
}

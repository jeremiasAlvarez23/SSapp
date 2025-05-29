using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SecuritysApp.Models.Usuario;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Routes;
using SecuritysApp.Utils;

namespace SecuritysApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : AppController
    {
        [HttpPost(AppRoutes.v1.Usuario.Insertar)]
        public IActionResult Insertar([FromBody] UsuarioRequest request)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            UsuarioGestor.Insertar(request, usuarioEjecutorId, ip, userAgent);
            return Ok(new { mensaje = "Usuario creado exitosamente" });
        }

        [HttpGet(AppRoutes.v1.Usuario.ObtenerTodo)]
        public IActionResult ObtenerTodo()
        {
            var usuarios = UsuarioGestor.ObtenerTodo();
            return Ok(usuarios);
        }

        [HttpGet(AppRoutes.v1.Usuario.ObtenerPorFiltro)]
        public IActionResult ObtenerPorFiltro([FromQuery] bool activo)
        {
            var usuarios = UsuarioGestor.ObtenerPorFiltro(activo);
            return Ok(usuarios);
        }

        [HttpGet(AppRoutes.v1.Usuario.ObtenerPorId)]
        public IActionResult ObtenerPorId(int id)
        {
            var usuario = UsuarioGestor.ObtenerPorId(id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpGet(AppRoutes.v1.Usuario.ObtenerConPaginacion)]
        public IActionResult ObtenerConPaginacion([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? busqueda = null, [FromQuery] bool? activo = null)
        {
            var usuarios = UsuarioGestor.ObtenerConPaginacion(skip, take, busqueda, activo);
            return Ok(usuarios);
        }

        [HttpPut(AppRoutes.v1.Usuario.Editar)]
        public IActionResult Editar(int id, [FromBody] UsuarioRequest request)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            UsuarioGestor.Editar(id, request, usuarioEjecutorId, ip, userAgent);
            return Ok(new { mensaje = "Usuario actualizado" });
        }

        [HttpPut(AppRoutes.v1.Usuario.Desactivar)]
        public IActionResult Desactivar(int id)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            UsuarioGestor.Desactivar(id, usuarioEjecutorId, ip, userAgent);
            return Ok(new { mensaje = "Usuario desactivado" });
        }

        [HttpPut(AppRoutes.v1.Usuario.Activar)]
        public IActionResult Activar(int id)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            UsuarioGestor.Activar(id, usuarioEjecutorId, ip, userAgent);
            return Ok(new { mensaje = "Usuario activado" });
        }
    }
}

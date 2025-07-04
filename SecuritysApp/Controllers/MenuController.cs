using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Models.Menu;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Routes;
using SecuritysApp.Utils;

namespace SecuritysApp.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MenuController : AppController
    {
        [HttpPost(AppRoutes.v1.Menu.Insertar)]
        public IActionResult Insertar(InsertarMenuRequest request)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();

            var result = MenuGestor.Insertar(request, usuarioEjecutorId, ip, userAgent);

            if (!result)
                return BadRequest(new { error = "Ya existe un menú con ese nombre." });

            string? carpeta = null;

            if (request.MenuPadreId.HasValue && request.MenuPadreId.Value > 0)
            {
                using var context = new SecuritysApp.Data.SecuritysContext();
                var menuPadre = context.Menu.FirstOrDefault(m => m.MenuId == request.MenuPadreId.Value);
                carpeta = menuPadre?.Nombre;
            }

            return Ok(new
            {
                mensaje = "Menú insertado.",
                carpeta = carpeta
            });
        }


        [HttpGet(AppRoutes.v1.Menu.ObtenerTodo)]
        public IActionResult ObtenerTodo()
        {
            return Ok(MenuGestor.ObtenerTodo());
        }

        // [HttpGet(AppRoutes.v1.Menu.ObtenerPorUsuario)]
        // public IActionResult ObtenerPorUsuario()
        // {
        //     var claim = User.Claims.FirstOrDefault(c => c.Type == "UsuarioId");

        //     if (claim == null)
        //         return Unauthorized("No se encontró el claim UsuarioId.");

        //     var usuarioId = int.Parse(claim.Value);

        //     return Ok(MenuGestor.ObtenerPorUsuario(usuarioId));
        // }
        [HttpGet(AppRoutes.v1.Menu.ObtenerPorUsuario)]
        public IActionResult ObtenerPorUsuario()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "UsuarioId");

            if (claim == null)
                return Unauthorized("No se encontró el claim UsuarioId.");

            var usuarioId = int.Parse(claim.Value);

            return Ok(MenuGestor.ObtenerPorUsuario(usuarioId));
        }

        [HttpGet(AppRoutes.v1.Menu.ObtenerPadres)]
        public IActionResult ObtenerPadres()
        {
            return Ok(MenuGestor.ObtenerPadres());
        }

        [HttpGet(AppRoutes.v1.Menu.ObtenerUltimoOrden)]
        public IActionResult ObtenerUltimoOrden(int padreId)
        {
            return Ok(MenuGestor.ObtenerUltimoOrden(padreId));
        }

        [HttpPut(AppRoutes.v1.Menu.Editar)]
        public IActionResult Editar(int id, InsertarMenuRequest request)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            return MenuGestor.Editar(id, request, usuarioEjecutorId, ip, userAgent) ? Ok("Menú editado.") : NotFound();
        }

        [HttpDelete(AppRoutes.v1.Menu.Eliminar)]
        public IActionResult Eliminar(int id)
        {
            var usuarioEjecutorId = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            return MenuGestor.Desactivar(id, usuarioEjecutorId, ip, userAgent) ? Ok("Menú eliminado.") : NotFound();
        }
    }
}

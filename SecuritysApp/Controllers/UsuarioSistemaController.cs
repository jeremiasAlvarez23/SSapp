using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Models.UsuarioSistema;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Routes;
using SecuritysApp.Utils;

namespace SecuritysApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1")]
    public class UsuarioSistemaController : AppController
    {
        [HttpPost(AppRoutes.v1.UsuarioSistema.Insertar)]
        public IActionResult Insertar([FromBody] UsuarioSistemaRequest request)
        {
            UsuarioSistemaGestor.Insertar(request);
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
            return UsuarioSistemaGestor.Editar(request) ? Ok("Relación actualizada.") : NotFound();
        }

        [HttpDelete(AppRoutes.v1.UsuarioSistema.Eliminar)]
        public IActionResult Eliminar([FromQuery] int usuarioId, [FromQuery] int sistemaId)
        {
            return UsuarioSistemaGestor.Eliminar(usuarioId, sistemaId) ? Ok("Relación eliminada.") : NotFound();
        }
    }
}

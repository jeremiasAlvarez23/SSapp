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
            SistemaGestor.Insertar(request);
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

        [HttpPut(AppRoutes.v1.Sistema.Editar)]
        public IActionResult Editar(int id, InsertarSistemaRequest request)
        {
            return SistemaGestor.Editar(id, request) ? Ok("Sistema editado.") : NotFound();
        }

        [HttpPut(AppRoutes.v1.Sistema.Activar)]
        public IActionResult Activar(int id)
        {
            return SistemaGestor.Activar(id) ? Ok("Sistema activado.") : NotFound();
        }

        [HttpDelete(AppRoutes.v1.Sistema.Desactivar)]
        public IActionResult Desactivar(int id)
        {
            return SistemaGestor.Desactivar(id) ? Ok("Sistema desactivado.") : NotFound();
        }

    }
}

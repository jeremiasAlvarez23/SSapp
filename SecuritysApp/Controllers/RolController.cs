using SecuritysApp.Models.Rol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Routes;
using SecuritysApp.Utils;


namespace SecuritysApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class RolController : AppController
    {
        [HttpPost(AppRoutes.v1.Rol.Insertar)]
        public IActionResult Insertar([FromBody] RolRequest request)
        {
            RolGestor.Insertar(request);
            return Ok(new { mensaje = "Rol creado exitosamente" });
        }

        [HttpGet(AppRoutes.v1.Rol.ObtenerTodo)]
        public IActionResult ObtenerTodo()
        {
            var roles = RolGestor.ObtenerTodo();
            return Ok(roles);
        }

        [HttpPut(AppRoutes.v1.Rol.Editar)]
        public IActionResult Editar(int id, [FromBody] RolRequest request)
        {
            RolGestor.Editar(id, request);
            return Ok(new { mensaje = "Rol actualizado" });
        }

        [HttpDelete(AppRoutes.v1.Rol.Eliminar)]
        public IActionResult Eliminar(int id)
        {
            RolGestor.Eliminar(id);
            return Ok(new { mensaje = "Rol eliminado" });
        }
    }
}

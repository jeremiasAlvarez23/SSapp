using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Negocio.Gestores;
using SecuritysApp.Models.Usuario;
using SecuritysApp.Routes;

namespace SecuritysApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(ApiRoutes.v1.Usuario.ObtenerTodo)]
        public IActionResult ObtenerTodo()
        {
            var usuarios = UsuarioGestor.ObtenerTodo();
            return Ok(usuarios);
        }
    }
}

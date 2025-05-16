using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuritysApp.Models.Login;
using SecuritysApp.Negocio.Gestores;
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
            var resultado = LoginGestor.AutenticarUsuario(request);

            if (resultado == null)
                return Unauthorized(new { mensaje = "Credenciales inválidas" });

            return Ok(resultado);
        }
    }
}

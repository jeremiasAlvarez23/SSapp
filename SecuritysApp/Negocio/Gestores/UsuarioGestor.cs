using System.Collections.Generic;
using System.Linq;
using SecuritysApp.Data;
using SecuritysApp.Models.Usuario;
using SecuritysApp.Negocio.Entidades;

namespace SecuritysApp.Negocio.Gestores
{
    public class UsuarioGestor
    {
        public static List<ObtenerTodoUsuarioResponse> ObtenerTodo()
        {
            using (var context = new SecuritysContext())
            {
                return context.Usuarios
                    .Select(u => new ObtenerTodoUsuarioResponse
                    {
                        UsuarioId = u.UsuarioId,
                        Nombre = u.Nombre,
                        Email = u.Email,
                        Activo = u.Activo
                    })
                    .ToList();
            }
        }
    }
}

using SecuritysApp.Negocio.Entidades;
using SecuritysApp.Models.Usuario;

namespace SecuritysApp.Negocio.Extensiones
{
    public static class UsuarioExtension
    {
        public static ObtenerTodoUsuarioResponse ToResponse(this Usuarios usuario)
        {
            return new ObtenerTodoUsuarioResponse
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Activo = usuario.Activo
            };
        }
    }
}

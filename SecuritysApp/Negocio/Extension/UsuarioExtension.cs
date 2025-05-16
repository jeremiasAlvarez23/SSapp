using SecuritysApp.Models.Usuario;
using SecuritysApp.Negocio.Entidades;

namespace SecuritysApp.Negocio.Extension
{
    public static class UsuarioExtension
    {
        public static UsuarioResponse ToResponse(this Usuario usuario)
        {
            return new UsuarioResponse
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Activo = usuario.Activo
            };
        }
    }
}

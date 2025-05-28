using SecuritysApp.Models.UsuarioSistema;
using SecuritysApp.Negocio.Entidades;

namespace SecuritysApp.Negocio.Extension
{
    public static class UsuarioSistemaExtension
    {
        public static UsuarioSistemaResponse ToResponse(this UsuarioSistema us)
        {
            return new UsuarioSistemaResponse
            {
                UsuarioId = us.UsuarioId,
                SistemaId = us.SistemaId,
                TieneAcceso = us.TieneAcceso,
                RolSistema = us.RolSistema,
                UsuarioNombre = us.Usuario?.Nombre ?? "",
                SistemaNombre = us.Sistema?.Nombre ?? ""
            };
        }
    }
}

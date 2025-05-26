using SecuritysApp.Models.Rol;
using SecuritysApp.Negocio.Entidades;

namespace SecuritysApp.Negocio.Extension
{
    public static class RolExtension
    {
        public static RolResponse ToResponse(this Rol rol)
        {
            return new RolResponse
            {
                RolId = rol.RolId,
                Nombre = rol.Nombre ?? string.Empty,
                Descripcion = rol.Descripcion ?? string.Empty
            };
        }
    }
}

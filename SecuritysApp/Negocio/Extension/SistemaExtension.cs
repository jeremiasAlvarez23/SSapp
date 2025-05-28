using SecuritysApp.Models.Sistema;
using SecuritysApp.Negocio.Entidades;

namespace SecuritysApp.Negocio.Extension
{
    public static class SistemaExtension
    {
        public static SistemaResponse ToResponse(this Sistema sistema)
        {
            return new SistemaResponse
            {
                SistemaId = sistema.SistemaId,
                Nombre = sistema.Nombre,
                Descripcion = sistema.Descripcion ?? string.Empty,
                UrlBase = sistema.UrlBase,
                Activo = sistema.Activo
            };
        }
    }
}

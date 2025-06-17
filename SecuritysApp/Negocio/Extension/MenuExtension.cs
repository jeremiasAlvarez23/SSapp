using SecuritysApp.Models.Menu;
using SecuritysApp.Negocio.Entidades;

namespace SecuritysApp.Negocio.Extension
{
    public static class MenuExtension
    {
        public static MenuResponse ToResponse(this Menu menu)
        {
            return new MenuResponse
            {
                MenuId = menu.MenuId,
                Nombre = menu.Nombre,
                Ruta = menu.Ruta,
                Componente = menu.Componente,
                Orden = menu.Orden,
                Icono = menu.Icono,
                Color = menu.Color,
                Visible = menu.Visible ?? true
            };
        }
    }
}

using SecuritysApp.Data;
using SecuritysApp.Models.Menu;
using SecuritysApp.Negocio.Extension;
using SecuritysApp.Auditoria.Gestores;

namespace SecuritysApp.Negocio.Gestores
{
    public static class MenuGestor
    {
        public static bool Insertar(InsertarMenuRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();

            var existe = context.Menu.Any(m => m.Nombre == request.Nombre && m.SistemaId == request.SistemaId && m.Activo == true);
            if (existe) return false;

            var menu = new Entidades.Menu
            {
                Nombre = request.Nombre,
                Ruta = request.Ruta,
                Componente = request.Componente,
                Icono = request.Icono,
                Color = request.Color,
                Orden = request.Orden ?? 0,
                MenuPadreId = request.MenuPadreId,
                Visible = request.Visible,
                SistemaId = request.SistemaId,
                Activo = true
            };

            context.Menu.Add(menu);
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Alta Menú", $"Se creó el menú {menu.Nombre}", "Menu", menu.MenuId.ToString(), null, ip, navegador);
            return true;
        }

        public static List<MenuResponse> ObtenerTodo()
        {
            using var context = new SecuritysContext();
            return context.Menu
                .Where(m => m.Activo == true)
                .Select(m => m.ToResponse())
                .ToList();
        }

        public static List<MenuResponse> ObtenerPadres()
        {
            using var context = new SecuritysContext();
            return context.Menu
                .Where(m => m.Activo == true && m.MenuPadreId == 0)
                .Select(m => m.ToResponse())
                .ToList();
        }

        public static int ObtenerUltimoOrden(int padreId)
        {
            using var context = new SecuritysContext();
            var max = context.Menu
                .Where(m => m.MenuPadreId == padreId && m.Activo == true)
                .OrderByDescending(m => m.Orden)
                .FirstOrDefault();

            return (max?.Orden ?? 0) + 1;
        }

        public static List<MenuResponse> ObtenerPorUsuario(int usuarioId)
        {
            using var context = new SecuritysContext();
            var usuarioSistema = context.UsuarioSistema.FirstOrDefault(us => us.UsuarioId == usuarioId && us.TieneAcceso == true);
            if (usuarioSistema == null)
                return new();

            if (!int.TryParse(usuarioSistema.RolSistema, out int rolId))
                return new();

            int sistemaId = usuarioSistema.SistemaId;

            var menues = context.RolMenu
                .Where(rm => rm.RolId == rolId && rm.Menu != null && rm.Menu.Activo == true && rm.Menu.SistemaId == sistemaId)
                .Select(rm => rm.Menu!)
                .ToList();

            var padres = menues
                .Where(m => m.MenuPadreId == 0)
                .OrderBy(m => m.Orden)
                .Select(padre => new MenuResponse
                {
                    MenuId = padre.MenuId,
                    Nombre = padre.Nombre ?? "",
                    Ruta = padre.Ruta ?? "",
                    Componente = padre.Componente ?? "",
                    Icono = padre.Icono ?? "",
                    Color = padre.Color ?? "",
                    Orden = padre.Orden ?? 0,
                    Visible = padre.Visible ?? false,
                    Submenu = menues
                        .Where(m => m.MenuPadreId == padre.MenuId)
                        .OrderBy(m => m.Orden)
                        .Select(m => new MenuResponse
                        {
                            MenuId = m.MenuId,
                            Nombre = m.Nombre ?? "",
                            Ruta = m.Ruta ?? "",
                            Componente = m.Componente ?? "",
                            Icono = m.Icono ?? "",
                            Color = m.Color ?? "",
                            Orden = m.Orden ?? 0,
                            Visible = m.Visible ?? false
                        }).ToList()
                }).ToList();

            return padres;
        }

        public static bool Editar(int id, InsertarMenuRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var menu = context.Menu.FirstOrDefault(m => m.MenuId == id && m.Activo == true);
            if (menu == null) return false;

            menu.Nombre = request.Nombre;
            menu.Componente = request.Componente;
            menu.Ruta = request.Ruta;
            menu.MenuPadreId = request.MenuPadreId;
            menu.Orden = request.Orden ?? 0;
            menu.Icono = request.Icono;
            menu.Color = request.Color;
            menu.Visible = request.Visible;

            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Editar Menú", $"Menú {menu.Nombre} editado", "Menu", menu.MenuId.ToString(), null, ip, navegador);
            return true;
        }

        public static bool Desactivar(int id, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var menu = context.Menu.FirstOrDefault(m => m.MenuId == id);
            if (menu == null) return false;

            menu.Activo = false;
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Eliminar Menú", $"Menú {menu.Nombre} eliminado", "Menu", menu.MenuId.ToString(), null, ip, navegador);
            return true;
        }
    }
}

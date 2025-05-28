using SecuritysApp.Data;
using SecuritysApp.Models.Sistema;
using SecuritysApp.Negocio.Extension;

namespace SecuritysApp.Negocio.Gestores
{
    public static class SistemaGestor
    {
        public static void Insertar(InsertarSistemaRequest request)
        {
            using var context = new SecuritysContext();
            var sistema = new Entidades.Sistema
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                UrlBase = request.UrlBase,
                Activo = true
            };
            context.Sistema.Add(sistema);
            context.SaveChanges();
        }

        public static List<SistemaResponse> ObtenerTodo()
        {
            using var context = new SecuritysContext();
            return context.Sistema
                .Select(s => s.ToResponse())
                .ToList();
        }

        public static SistemaResponse? ObtenerPorId(int id)
        {
            using var context = new SecuritysContext();
            return context.Sistema.FirstOrDefault(s => s.SistemaId == id)?.ToResponse();
        }

        public static List<SistemaResponse> ObtenerConPaginacion(int skip, int take, string? busqueda, bool? activo)
        {
            using var context = new SecuritysContext();
            var query = context.Sistema.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                query = query.Where(s => s.Nombre.Contains(busqueda) || (s.Descripcion != null && s.Descripcion.Contains(busqueda)));
            }

            if (activo.HasValue)
            {
                query = query.Where(s => s.Activo == activo);
            }

            return query
                .OrderBy(s => s.Nombre)
                .Skip(skip)
                .Take(take)
                .Select(s => s.ToResponse())
                .ToList();
        }
        public static bool Editar(int id, InsertarSistemaRequest request)
        {
            using var context = new SecuritysContext();
            var sistema = context.Sistema.FirstOrDefault(s => s.SistemaId == id);
            if (sistema == null) return false;

            sistema.Nombre = request.Nombre;
            sistema.Descripcion = request.Descripcion;
            sistema.UrlBase = request.UrlBase;
            context.SaveChanges();
            return true;
        }

        public static bool Desactivar(int id)
        {
            using var context = new SecuritysContext();
            var sistema = context.Sistema.FirstOrDefault(s => s.SistemaId == id);
            if (sistema == null) return false;

            sistema.Activo = false;
            context.SaveChanges();
            return true;
        }

        public static bool Activar(int id)
        {
            using var context = new SecuritysContext();
            var sistema = context.Sistema.FirstOrDefault(s => s.SistemaId == id);
            if (sistema == null) return false;

            sistema.Activo = true;
            context.SaveChanges();
            return true;
        }

    }
}

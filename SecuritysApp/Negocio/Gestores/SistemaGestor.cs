using SecuritysApp.Data;
using SecuritysApp.Models.Sistema;
using SecuritysApp.Negocio.Extension;
using SecuritysApp.Auditoria.Gestores;

namespace SecuritysApp.Negocio.Gestores
{
    public static class SistemaGestor
    {
        public static void Insertar(InsertarSistemaRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
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

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Alta Sistema", $"Se creó el sistema {sistema.Nombre}", "Sistema", sistema.SistemaId.ToString(), null, ip, navegador);
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

        public static bool Editar(int id, InsertarSistemaRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var sistema = context.Sistema.FirstOrDefault(s => s.SistemaId == id);
            if (sistema == null) return false;

            var datosAnteriores = $"Nombre: {sistema.Nombre}, Descripcion: {sistema.Descripcion}, UrlBase: {sistema.UrlBase}";

            sistema.Nombre = request.Nombre;
            sistema.Descripcion = request.Descripcion;
            sistema.UrlBase = request.UrlBase;
            context.SaveChanges();

            var datosNuevos = $"Nombre: {sistema.Nombre}, Descripcion: {sistema.Descripcion}, UrlBase: {sistema.UrlBase}";
            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Editar Sistema", $"Sistema {sistema.Nombre} editado", "Sistema", sistema.SistemaId.ToString(), null, ip, navegador);
            return true;
        }

        public static bool Desactivar(int id, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var sistema = context.Sistema.FirstOrDefault(s => s.SistemaId == id);
            if (sistema == null) return false;

            sistema.Activo = false;
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Desactivar Sistema", $"Sistema {sistema.Nombre} desactivado", "Sistema", sistema.SistemaId.ToString(), null, ip, navegador);
            return true;
        }

        public static bool Activar(int id, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var sistema = context.Sistema.FirstOrDefault(s => s.SistemaId == id);
            if (sistema == null) return false;

            sistema.Activo = true;
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Activar Sistema", $"Sistema {sistema.Nombre} activado", "Sistema", sistema.SistemaId.ToString(), null, ip, navegador);
            return true;
        }
    }
}

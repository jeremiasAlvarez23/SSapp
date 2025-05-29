using SecuritysApp.Data;
using SecuritysApp.Models.Rol;
using SecuritysApp.Negocio.Entidades;
using SecuritysApp.Negocio.Extension;
using SecuritysApp.Auditoria.Gestores;

namespace SecuritysApp.Negocio.Gestores
{
    public static class RolGestor
    {
        public static void Insertar(RolRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var nuevo = new Rol
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
            context.Rol.Add(nuevo);
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Alta Rol", $"Se creó el rol {nuevo.Nombre}", "Rol", nuevo.RolId.ToString(), null, ip, navegador);
        }

        public static List<RolResponse> ObtenerTodo()
        {
            using var context = new SecuritysContext();
            return context.Rol.Select(r => r.ToResponse()).ToList();
        }

        public static void Editar(int id, RolRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var rol = context.Rol.FirstOrDefault(r => r.RolId == id);
            if (rol == null) return;

            var datosAnteriores = $"Nombre: {rol.Nombre}, Descripcion: {rol.Descripcion}";
            rol.Nombre = request.Nombre;
            rol.Descripcion = request.Descripcion;
            context.SaveChanges();

            var datosNuevos = $"Nombre: {rol.Nombre}, Descripcion: {rol.Descripcion}";
            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Editar Rol", $"Rol {rol.Nombre} editado", "Rol", rol.RolId.ToString(), null, ip, navegador);
        }

        public static void Eliminar(int id, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var rol = context.Rol.FirstOrDefault(r => r.RolId == id);
            if (rol == null) return;

            context.Rol.Remove(rol);
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Eliminar Rol", $"Rol {rol.Nombre} eliminado", "Rol", rol.RolId.ToString(), null, ip, navegador);
        }
    }
}

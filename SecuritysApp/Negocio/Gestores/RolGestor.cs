using SecuritysApp.Data;
using SecuritysApp.Models.Rol;
using SecuritysApp.Negocio.Entidades;
using SecuritysApp.Negocio.Extension;

namespace SecuritysApp.Negocio.Gestores
{
    public static class RolGestor
    {
        public static void Insertar(RolRequest request)
        {
            using var context = new SecuritysContext();
            var nuevo = new Rol
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
            context.Rol.Add(nuevo);
            context.SaveChanges();
        }

        public static List<RolResponse> ObtenerTodo()
        {
            using var context = new SecuritysContext();
            return context.Rol.Select(r => r.ToResponse()).ToList();
        }

        public static void Editar(int id, RolRequest request)
        {
            using var context = new SecuritysContext();
            var rol = context.Rol.FirstOrDefault(r => r.RolId == id);
            if (rol == null) return;
            rol.Nombre = request.Nombre;
            rol.Descripcion = request.Descripcion;
            context.SaveChanges();
        }

        public static void Eliminar(int id)
        {
            using var context = new SecuritysContext();
            var rol = context.Rol.FirstOrDefault(r => r.RolId == id);
            if (rol == null) return;
            context.Rol.Remove(rol);
            context.SaveChanges();
        }
    }
}

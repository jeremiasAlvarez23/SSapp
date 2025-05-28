using Microsoft.EntityFrameworkCore;
using SecuritysApp.Data;
using SecuritysApp.Models.Usuario;
using SecuritysApp.Negocio.Entidades;
using SecuritysApp.Negocio.Extension;
using SecuritysApp.Utils;

namespace SecuritysApp.Negocio.Gestores
{
    public static class UsuarioGestor
    {
        public static void Insertar(UsuarioRequest request)
        {
            using var context = new SecuritysContext();

            if (context.Usuario.Any(u => u.Email == request.Email))
                throw new ArgumentException("Ya existe un usuario con ese email");

            if (!context.Rol.Any(r => r.RolId == request.RolId))
                throw new ArgumentException("El rol especificado no existe");

            var nuevo = new Usuario
            {
                Nombre = request.Nombre,
                Email = request.Email,
                PasswordHash = PasswordHelper.Encriptar(request.Clave),
                RolId = request.RolId,
                Activo = true
            };

            context.Usuario.Add(nuevo);
            context.SaveChanges();
        }

        public static List<UsuarioResponse> ObtenerTodo()
        {
            using var context = new SecuritysContext();
            return context.Usuario
                .Include(u => u.Rol)
                .Select(u => u.ToResponse())
                .ToList();
        }

        public static List<UsuarioResponse> ObtenerPorFiltro(bool activo)
        {
            using var context = new SecuritysContext();

            return context.Usuario
                .Where(u => u.Activo == activo)
                .Include(u => u.Rol)
                .Select(u => u.ToResponse())
                .ToList();
        }

        public static UsuarioResponse? ObtenerPorId(int id)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.UsuarioId == id);

            return usuario?.ToResponse();
        }

        public static void Editar(int id, UsuarioRequest request)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null) throw new ArgumentException("Usuario no encontrado");

            if (context.Usuario.Any(u => u.Email == request.Email && u.UsuarioId != id))
                throw new ArgumentException("Ese email ya está siendo utilizado por otro usuario");

            if (!context.Rol.Any(r => r.RolId == request.RolId))
                throw new ArgumentException("El rol especificado no existe");

            usuario.Nombre = request.Nombre;
            usuario.Email = request.Email;
            usuario.RolId = request.RolId;

            if (!string.IsNullOrWhiteSpace(request.Clave))
            {
                usuario.PasswordHash = PasswordHelper.Encriptar(request.Clave);
            }

            context.SaveChanges();
        }

        public static void Desactivar(int id)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null) throw new ArgumentException("Usuario no encontrado");

            usuario.Activo = false;
            context.SaveChanges();
        }

        public static void Activar(int id)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null) throw new ArgumentException("Usuario no encontrado");

            usuario.Activo = true;
            context.SaveChanges();
        }
    }
}

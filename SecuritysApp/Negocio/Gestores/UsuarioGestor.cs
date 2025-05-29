using Microsoft.EntityFrameworkCore;
using SecuritysApp.Data;
using SecuritysApp.Models.Usuario;
using SecuritysApp.Negocio.Entidades;
using SecuritysApp.Negocio.Extension;
using SecuritysApp.Utils;
using SecuritysApp.Auditoria.Gestores;

namespace SecuritysApp.Negocio.Gestores
{
    public static class UsuarioGestor
    {
        public static void Insertar(UsuarioRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
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

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Alta Usuario", $"Se creó el usuario {nuevo.Email}", "Usuario", nuevo.UsuarioId.ToString(), nuevo.UsuarioId, ip, navegador);
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

        public static List<UsuarioResponse> ObtenerConPaginacion(int skip, int take, string? busqueda, bool? activo)
        {
            using var context = new SecuritysContext();
            var query = context.Usuario
                .Include(u => u.Rol)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(busqueda))
                query = query.Where(u => u.Nombre.Contains(busqueda) || u.Email.Contains(busqueda));

            if (activo.HasValue)
                query = query.Where(u => u.Activo == activo);

            return query.OrderBy(u => u.Nombre).Skip(skip).Take(take).Select(u => u.ToResponse()).ToList();
        }

        public static void Editar(int id, UsuarioRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var usuario = context.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null) throw new ArgumentException("Usuario no encontrado");

            if (context.Usuario.Any(u => u.Email == request.Email && u.UsuarioId != id))
                throw new ArgumentException("Ese email ya está siendo utilizado por otro usuario");

            if (!context.Rol.Any(r => r.RolId == request.RolId))
                throw new ArgumentException("El rol especificado no existe");

            var datosAnteriores = $"Nombre: {usuario.Nombre}, Email: {usuario.Email}, RolId: {usuario.RolId}";
            usuario.Nombre = request.Nombre;
            usuario.Email = request.Email;
            usuario.RolId = request.RolId;

            if (!string.IsNullOrWhiteSpace(request.Clave))
                usuario.PasswordHash = PasswordHelper.Encriptar(request.Clave);

            context.SaveChanges();

            var datosNuevos = $"Nombre: {usuario.Nombre}, Email: {usuario.Email}, RolId: {usuario.RolId}";
            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Editar Usuario", $"Usuario {usuario.Email} editado", "Usuario", usuario.UsuarioId.ToString(), usuario.UsuarioId, ip, navegador);
        }

        public static void Desactivar(int id, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var usuario = context.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null) throw new ArgumentException("Usuario no encontrado");

            usuario.Activo = false;
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Desactivar Usuario", $"Se desactivó al usuario {usuario.Email}", "Usuario", usuario.UsuarioId.ToString(), usuario.UsuarioId, ip, navegador);
        }

        public static void Activar(int id, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var usuario = context.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null) throw new ArgumentException("Usuario no encontrado");

            usuario.Activo = true;
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Activar Usuario", $"Se activó al usuario {usuario.Email}", "Usuario", usuario.UsuarioId.ToString(), usuario.UsuarioId, ip, navegador);
        }
    }
}

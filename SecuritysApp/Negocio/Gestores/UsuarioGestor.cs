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

            var existe = context.Usuario.Any(u => u.Email == request.Email);
            if (existe)
                throw new Exception("Ya existe un usuario con ese email");

            var nuevo = new Usuario
            {
                Nombre = request.Nombre,
                Email = request.Email,
                PasswordHash = PasswordHelper.Encriptar(request.Clave),
                Activo = true
            };

            context.Usuario.Add(nuevo);
            context.SaveChanges();
        }

        public static List<UsuarioResponse> ObtenerTodo()
        {
            using var context = new SecuritysContext();
            return context.Usuario
                .Select(u => u.ToResponse())
                .ToList();
        }

        public static List<UsuarioResponse> ObtenerPorFiltro(bool activo)
        {
            using var context = new SecuritysContext();

            return context.Usuario
                .Where(u => u.Activo == activo)
                .Select(u => u.ToResponse())
                .ToList();
        }

        public static UsuarioResponse? ObtenerPorId(int id)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario
                .FirstOrDefault(u => u.UsuarioId == id);

            return usuario?.ToResponse();
        }

        public static void Editar(int id, UsuarioRequest request)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null) return;

            var emailUsado = context.Usuario.Any(u => u.Email == request.Email && u.UsuarioId != id);
            if (emailUsado)
                throw new Exception("Ese email ya está siendo utilizado por otro usuario");

            usuario.Nombre = request.Nombre;
            usuario.Email = request.Email;

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

            if (usuario == null) return;

            usuario.Activo = false;
            context.SaveChanges();
        }

        public static void Activar(int id)
        {
            using var context = new SecuritysContext();

            var usuario = context.Usuario.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario == null) return;

            usuario.Activo = true;
            context.SaveChanges();
        }

    }
}

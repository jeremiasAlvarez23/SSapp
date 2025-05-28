using Microsoft.EntityFrameworkCore;
using SecuritysApp.Data;
using SecuritysApp.Models.UsuarioSistema;
using SecuritysApp.Negocio.Extension;

namespace SecuritysApp.Negocio.Gestores
{
    public static class UsuarioSistemaGestor
    {
        public static void Insertar(UsuarioSistemaRequest request)
        {
            using var context = new SecuritysContext();
            var existe = context.UsuarioSistema.Any(us => us.UsuarioId == request.UsuarioId && us.SistemaId == request.SistemaId);
            if (existe)
                throw new ArgumentException("La relación Usuario-Sistema ya existe.");

            var usuarioSistema = new Entidades.UsuarioSistema
            {
                UsuarioId = request.UsuarioId,
                SistemaId = request.SistemaId,
                TieneAcceso = request.TieneAcceso,
                RolSistema = request.RolSistema
            };
            context.UsuarioSistema.Add(usuarioSistema);
            context.SaveChanges();
        }

        public static List<UsuarioSistemaResponse> ObtenerTodo()
        {
            using var context = new SecuritysContext();
            return context.UsuarioSistema
                .Include(us => us.Usuario)
                .Include(us => us.Sistema)
                .Select(us => us.ToResponse())
                .ToList();
        }

        public static bool Editar(UsuarioSistemaRequest request)
        {
            using var context = new SecuritysContext();
            var us = context.UsuarioSistema.FirstOrDefault(us => us.UsuarioId == request.UsuarioId && us.SistemaId == request.SistemaId);
            if (us == null) return false;

            us.TieneAcceso = request.TieneAcceso;
            us.RolSistema = request.RolSistema;
            context.SaveChanges();
            return true;
        }

        public static bool Eliminar(int usuarioId, int sistemaId)
        {
            using var context = new SecuritysContext();
            var us = context.UsuarioSistema.FirstOrDefault(us => us.UsuarioId == usuarioId && us.SistemaId == sistemaId);
            if (us == null) return false;

            context.UsuarioSistema.Remove(us);
            context.SaveChanges();
            return true;
        }
    }
}

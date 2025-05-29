using Microsoft.EntityFrameworkCore;
using SecuritysApp.Data;
using SecuritysApp.Models.UsuarioSistema;
using SecuritysApp.Negocio.Extension;
using SecuritysApp.Auditoria.Gestores;

namespace SecuritysApp.Negocio.Gestores
{
    public static class UsuarioSistemaGestor
    {
        public static void Insertar(UsuarioSistemaRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
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

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Alta UsuarioSistema", $"Asignado sistema {request.SistemaId} al usuario {request.UsuarioId}", "UsuarioSistema", $"{request.UsuarioId}-{request.SistemaId}", request.UsuarioId, ip, navegador);
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

        public static bool Editar(UsuarioSistemaRequest request, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var us = context.UsuarioSistema.FirstOrDefault(us => us.UsuarioId == request.UsuarioId && us.SistemaId == request.SistemaId);
            if (us == null) return false;

            var datosAnteriores = $"TieneAcceso: {us.TieneAcceso}, RolSistema: {us.RolSistema}";

            us.TieneAcceso = request.TieneAcceso;
            us.RolSistema = request.RolSistema;
            context.SaveChanges();

            var datosNuevos = $"TieneAcceso: {us.TieneAcceso}, RolSistema: {us.RolSistema}";
            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Editar UsuarioSistema", $"Editado sistema {request.SistemaId} del usuario {request.UsuarioId}", "UsuarioSistema", $"{request.UsuarioId}-{request.SistemaId}", request.UsuarioId, ip, navegador);
            return true;
        }

        public static bool Eliminar(int usuarioId, int sistemaId, int usuarioEjecutorId, string? ip = null, string? navegador = null)
        {
            using var context = new SecuritysContext();
            var us = context.UsuarioSistema.FirstOrDefault(us => us.UsuarioId == usuarioId && us.SistemaId == sistemaId);
            if (us == null) return false;

            context.UsuarioSistema.Remove(us);
            context.SaveChanges();

            AuditoriaGestor.RegistrarEvento(usuarioEjecutorId, "Eliminar UsuarioSistema", $"Eliminada relación Usuario {usuarioId} - Sistema {sistemaId}", "UsuarioSistema", $"{usuarioId}-{sistemaId}", usuarioId, ip, navegador);
            return true;
        }
    }
}

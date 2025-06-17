using SecuritysApp.Models.Auditoria.Data;
using SecuritysApp.Models.Auditoria.Entidades;

namespace SecuritysApp.Auditoria.Gestores
{
    public static class AuditoriaGestor
    {
        /// <summary>
        /// Registra un evento completo con detalles del ejecutor, afectado y entidad.
        /// </summary>
        public static void RegistrarEvento(
            int usuarioEjecutorId, 
            string accion, 
            string descripcion, 
            string? entidad = null, 
            string? entidadId = null, 
            int? usuarioAfectadoId = null, 
            string? ip = null, 
            string? navegador = null, 
            string? sistema = "SecuritysApp", 
            bool exitoso = true)
        {
            using var context = new AuditoriaContext();
            var evento = new AuditoriaMain
            {
                UsuarioId = usuarioEjecutorId,
                Accion = accion,
                Descripcion = descripcion,
                Ip = ip,
                Navegador = navegador,
                Sistema = sistema,
                Fecha = DateTime.UtcNow,
                Exitoso = exitoso
            };

            // Agregar detalles adicionales en la descripción
            if (!string.IsNullOrWhiteSpace(entidad))
            {
                evento.Descripcion += $" | Entidad: {entidad}";
                if (!string.IsNullOrWhiteSpace(entidadId))
                {
                    evento.Descripcion += $" (ID: {entidadId})";
                }
            }

            if (usuarioAfectadoId.HasValue)
            {
                evento.Descripcion += $" | Afectado UsuarioId: {usuarioAfectadoId}";
            }

            context.AuditoriaMain.Add(evento);
            context.SaveChanges();
        }

        /// <summary>
        /// Registra un evento simple, solo con el usuario ejecutor y descripción.
        /// </summary>
        public static void RegistrarEventoSimple(int usuarioEjecutorId, string accion, string descripcion, string? ip = null, string? navegador = null, string? sistema = "SecuritysApp", bool exitoso = true)
        {
            RegistrarEvento(usuarioEjecutorId, accion, descripcion, null, null, null, ip, navegador, sistema, exitoso);
        }
    }
}




// using SecuritysApp.Auditoria.Data;
// using SecuritysApp.Auditoria.Entidades;

// namespace SecuritysApp.Auditoria.Gestores
// {
//     public static class AuditoriaGestor
//     {
//         /// <summary>
//         /// Registra un evento simple en la auditoría.
//         /// </summary>
//         public static void RegistrarEventoSimple(int? usuarioId, string accion, string descripcion, string? ip = null, string? navegador = null, string? sistema = "SecuritysApp", bool exitoso = true)
//         {
//             using var context = new AuditoriaContext();
//             var evento = new AuditoriaMain
//             {
//                 UsuarioId = usuarioId,
//                 Accion = accion,
//                 Descripcion = descripcion,
//                 Ip = ip,
//                 Navegador = navegador,
//                 Sistema = sistema,
//                 Fecha = DateTime.UtcNow,
//                 Exitoso = exitoso
//             };
//             context.AuditoriaMain.Add(evento);
//             context.SaveChanges();
//         }

//         /// <summary>
//         /// Registra un evento detallado con información adicional (ideal para ABM).
//         /// </summary>
//         public static void RegistrarEventoCompleto(int? usuarioId, string accion, string descripcion, string entidad, string? entidadId, string? datosAnteriores, string? datosNuevos, string? ip = null, string? navegador = null, string? sistema = "SecuritysApp", bool exitoso = true)
//         {
//             using var context = new AuditoriaContext();
//             var evento = new AuditoriaMain
//             {
//                 UsuarioId = usuarioId,
//                 Accion = accion,
//                 Descripcion = $"{descripcion} | Entidad: {entidad} (ID: {entidadId})",
//                 Ip = ip,
//                 Navegador = navegador,
//                 Sistema = sistema,
//                 Fecha = DateTime.UtcNow,
//                 Exitoso = exitoso
//             };
//             context.AuditoriaMain.Add(evento);
//             context.SaveChanges();
//         }
//     }
// }

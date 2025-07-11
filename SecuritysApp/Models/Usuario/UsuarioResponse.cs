namespace SecuritysApp.Models.Usuario
{
    public class UsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "Usuario";
        public bool Activo { get; set; }
    }
}

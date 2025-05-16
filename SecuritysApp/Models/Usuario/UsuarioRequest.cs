namespace SecuritysApp.Models.Usuario
{
    public class UsuarioRequest
    {
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Clave { get; set; } = null!;
    }
}

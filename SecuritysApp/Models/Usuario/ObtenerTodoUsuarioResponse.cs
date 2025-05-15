namespace SecuritysApp.Models.Usuario
{
    public class ObtenerTodoUsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
    }
}

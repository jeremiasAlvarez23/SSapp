namespace SecuritysApp.Models.Login
{
    public class LoginResponse
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> SistemasPermitidos { get; set; } = new();
        public string Token { get; set; } = null!;
    }
}

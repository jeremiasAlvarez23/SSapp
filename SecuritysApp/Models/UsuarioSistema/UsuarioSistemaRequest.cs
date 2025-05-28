namespace SecuritysApp.Models.UsuarioSistema
{
    public class UsuarioSistemaRequest
    {
        public int UsuarioId { get; set; }
        public int SistemaId { get; set; }
        public bool TieneAcceso { get; set; }
        public string RolSistema { get; set; } = null!;
    }
}

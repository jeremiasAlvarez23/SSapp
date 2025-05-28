namespace SecuritysApp.Models.UsuarioSistema
{
    public class UsuarioSistemaResponse
    {
        public int UsuarioId { get; set; }
        public int SistemaId { get; set; }
        public bool TieneAcceso { get; set; }
        public string RolSistema { get; set; } = string.Empty;
        public string UsuarioNombre { get; set; } = string.Empty;
        public string SistemaNombre { get; set; } = string.Empty;
    }
}

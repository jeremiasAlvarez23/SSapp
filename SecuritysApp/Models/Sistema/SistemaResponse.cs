namespace SecuritysApp.Models.Sistema
{
    public class SistemaResponse
    {
        public int SistemaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string UrlBase { get; set; } = null!;
        public bool Activo { get; set; }
    }
}

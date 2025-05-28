namespace SecuritysApp.Models.Sistema
{
    public class InsertarSistemaRequest
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string UrlBase { get; set; } = null!;
    }
}

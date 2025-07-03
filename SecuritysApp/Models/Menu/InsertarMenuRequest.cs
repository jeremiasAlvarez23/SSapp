namespace SecuritysApp.Models.Menu
{
    public class InsertarMenuRequest
    {
        public string? Nombre { get; set; } = null!;
        public string? Ruta { get; set; }
        public string? Icono { get; set; } = null;
        public string? Color { get; set; } = null;
        public string? Componente { get; set; } = null;
        public int? MenuPadreId { get; set; } = 0;

        public bool Visible { get; set; }
        public int SistemaId { get; set; }
        public int? Orden { get; set; } = 0;
    }
}




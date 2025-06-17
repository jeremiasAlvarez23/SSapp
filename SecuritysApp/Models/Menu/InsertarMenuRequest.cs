namespace SecuritysApp.Models.Menu
{
    public class InsertarMenuRequest
    {
        public string Nombre { get; set; } = null!;
        public string? Ruta { get; set; }
        public string? Componente { get; set; }
        public int MenuPadreId { get; set; } = 0;
        public int? Orden { get; set; }
        public string? Icono { get; set; }
        public string? Color { get; set; }
        public bool Visible { get; set; } = true;
        public int SistemaId { get; set; }
    }
}

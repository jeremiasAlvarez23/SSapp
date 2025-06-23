namespace SecuritysApp.Models.Menu
{
    public class MenuResponse
    {
        public int MenuId { get; set; }
        public int MenuPadreId { get; set; } 
        public string Nombre { get; set; } = null!;
        public string? Ruta { get; set; }
        public string? Componente { get; set; }
        public int? Orden { get; set; }
        public string? Icono { get; set; }
        public string? Color { get; set; }
        public bool Visible { get; set; } = true;

        public List<MenuResponse>? Submenu { get; set; }
    }
}

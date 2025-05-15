using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using SecuritysApp.Data; // este namespace lo definiremos cuando creemos tu DbContext

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar las variables del archivo .env
Env.Load();

// 2. Obtener la cadena de conexión
var connectionString = Environment.GetEnvironmentVariable("CADENA");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("La variable de entorno 'CADENA' no está definida en el .env");
}

// 3. Agregar DbContext con SQL Server
builder.Services.AddDbContext<SecuritysContext>(options =>
    options.UseSqlServer(connectionString));

// Servicios básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "SecuritysApp API",
        Version = "v1",
        Description = "Documentación interactiva de la API del sistema SecuritysApp",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "SecuritysApp",
            Email = "Contact@SecuritysApp.com"
        }
    });
});


// 4. Crear app
var app = builder.Build();

// Activa Swagger SIEMPRE (no solo en desarrollo)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SecuritysApp API v1");
    options.RoutePrefix = string.Empty; // Mostrará Swagger directamente en la raíz
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using SecuritysApp.Data;
using SecuritysApp.Models.Auditoria.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar variables del archivo .env
Env.Load();

// 2. Obtener la cadena de conexión para la base principal
var connectionString = Environment.GetEnvironmentVariable("CADENA");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("La variable de entorno 'CADENA' no está definida en el .env");
}

// 2.b Obtener la cadena de conexión para la base de auditoría
var auditoriaConnectionString = Environment.GetEnvironmentVariable("CADENA_AUDITORIA");
if (string.IsNullOrEmpty(auditoriaConnectionString))
{
    throw new Exception("La variable 'CADENA_AUDITORIA' no está definida en el .env");
}

// 3. Obtener la clave JWT
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
if (string.IsNullOrEmpty(jwtSecret) || jwtSecret.Length < 32)
{
    throw new Exception("La variable 'JWT_SECRET_KEY' no está definida o es demasiado corta.");
}
var key = Encoding.UTF8.GetBytes(jwtSecret);

// 4. Agregar DbContexts con SQL Server
builder.Services.AddDbContext<SecuritysContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<AuditoriaContext>(options =>
    options.UseSqlServer(auditoriaConnectionString));

// 5. Configurar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Solo para desarrollo (ajustar en producción)
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "SecuritysApp",
        ValidAudience = "SecuritysApp",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// 6. Servicios básicos
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

    var jwtSecurityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Description = "Autenticación JWT Bearer",
        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()      // O usa WithOrigins("http://localhost:8080")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// 7. Crear app
var app = builder.Build();

// Activa Swagger siempre
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SecuritysApp API v1");
    options.RoutePrefix = string.Empty;
});

// Middleware
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

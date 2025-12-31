// Program.cs - API REST CorpManager (Sesión 15)
using CorpManager_Completo.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de controladores (Sesión 15)
builder.Services.AddControllers();

// Registrar PersonaService como Singleton para mantener datos en memoria
builder.Services.AddSingleton<PersonaService>();

// Configurar Swagger/OpenAPI para documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CorpManager API",
        Version = "v1",
        Description = "API REST para gestión corporativa - Sesión 15"
    });
});

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CorpManager API v1");
        options.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

// Mapear controladores (Sesión 15)
app.MapControllers();

Console.WriteLine("CorpManager API iniciada en http://localhost:5000");
Console.WriteLine("Swagger UI disponible en http://localhost:5000");

app.Run();

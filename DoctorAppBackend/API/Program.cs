using API.Extensiones;
using API.Middleware;
using Data.Inicializador;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// --  Extensiones de servicios ---
// Sirve para agregar los servicios de la aplicación definidos en la extensión
builder.Services.AgregarServiciosAplicacion(builder.Configuration); 

// Sirve para agregar los servicios de identidad definidos en la extensión
builder.Services.AgregarServiciosIdentidad(builder.Configuration);
// -- Fin extensiones de servicios ---

builder.Services.AddScoped<IdbInicializador, DbInicializador>();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Middleware personalizado para manejar excepciones globalmente
app.UseMiddleware<ExceptionMiddleware>();

// Manejo de páginas de estado para errores HTTP
app.UseStatusCodePagesWithReExecute("/errores/{0}"); // Redirige las respuestas de error a un controlador específico

// Configuración de Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sirve para que el API pueda ser consumido desde el front-end en Angular
app.UseCors(x =>
{
    x.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

// Inicialización de la base de datos al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var inicializador = services.GetRequiredService<IdbInicializador>();
        inicializador.Inicializar();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Un Error ocurrio al ejecutar la migracion");
    }
}

app.MapControllers();
app.Run();


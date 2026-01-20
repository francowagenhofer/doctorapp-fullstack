using API.Errores;
using Data;
using Data.Interfaces;
using Data.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensiones
{
    // Clase de extensión para agregar servicios de la aplicación
    // Sirve para organizar y modularizar la configuración de servicios
    // Se invoca en Program.cs
    public static class ServicioAplicacionExtension
    {
        public static IServiceCollection AgregarServiciosAplicacion(this IServiceCollection servicios, IConfiguration config)
        {
            // Configuración de Swagger para incluir la autenticación JWT Bearer
            servicios.AddEndpointsApiExplorer();

            // Configuración de Swagger para incluir la autenticación JWT Bearer
            servicios.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingreasr Bearer [espacio] token \r\n\r\n " +
                                  "Ejemplo: Bearer ejoy^88788999990000",

                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                                In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            // Configurar el DbContext con SQL Server
            var connectionString = config.GetConnectionString("DefaultConnection");

            // Agregar el DbContext al contenedor de servicios
            servicios.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));

            // Configurar CORS para permitir solicitudes desde cualquier origen (Angular en este caso)
            servicios.AddCors();

            // Inyectar el servicio de token
            servicios.AddScoped<ITokenServicio, TokenServicio>();

            // Configurar respuestas de validación de modelos personalizadas.
            // Sirve para devolver errores de validación en un formato específico
            servicios.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errores = actionContext.ModelState // Obtiene el estado del modelo
                        .Where(e => e.Value.Errors.Count > 0) // Filtra las entradas con errores
                        .SelectMany(x => x.Value.Errors) // Selecciona todos los errores
                        .Select(x => x.ErrorMessage).ToArray(); // Proyecta los mensajes de error en un array

                    var errorResponse = new ApiValidacionErrorResponse // Crea una instancia de la respuesta de error personalizada
                    {
                        Errores = errores // Asigna los mensajes de error al objeto de respuesta
                    };

                    return new BadRequestObjectResult(errorResponse); 
                };
            });


            return servicios;
        }
    }
}

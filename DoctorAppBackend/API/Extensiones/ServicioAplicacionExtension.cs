using Data;
using Data.Interfaces;
using Data.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensiones
{
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


            return servicios;
        }
    }
}

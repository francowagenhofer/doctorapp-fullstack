using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensiones
{
    // Clase de extensión para agregar servicios de identidad
    // Sirve para organizar y modularizar la configuración de servicios relacionados con la identidad
    // Se invoca en Program.cs
    public static class ServicioIdentidadExtension
    {
        public static IServiceCollection AgregarServiciosIdentidad(this IServiceCollection servicios, IConfiguration config)
        {
            // Aquí puedes agregar servicios relacionados con la identidad, autenticación y autorización
            // Por ejemplo, puedes configurar Identity, JWT, políticas de autorización, etc.

            // Configurar la autenticación con JWT Bearer 
            servicios.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Configura los parámetros de validación del token
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Valida la clave de firma del emisor 
                        ValidateIssuerSigningKey = true,

                        // Clave secreta para validar la firma del token
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["TokenKey"])),

                        // No valida el emisor
                        ValidateIssuer = false,

                        // No valida la audiencia
                        ValidateAudience = false
                    };
                });

            return servicios;
        }
    }
}

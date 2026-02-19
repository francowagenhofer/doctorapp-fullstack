using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;

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

            servicios.AddIdentityCore<UsuarioAplicacion>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 6;
            })
                .AddRoles<RolAplicacion>()
                .AddRoleManager<RoleManager<RolAplicacion>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

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


            servicios.AddAuthorization(o=>
            {
                o.AddPolicy("AdminRol", politica => politica.RequireRole("Admin"));
                o.AddPolicy("AdminAgendadorRol", politica => politica.RequireRole("Admin", "Agendador"));
                o.AddPolicy("AdminDoctorRol", politica => politica.RequireRole("Doctor"));
            });

            return servicios;
        }
    }
}

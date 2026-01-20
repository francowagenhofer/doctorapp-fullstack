using API.Errores;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    // Clase de middleware para manejar excepciones globalmente
    // Captura las excepciones no manejadas en la tubería de solicitudes HTTP
    // Sirve para registrar el error y devolver una respuesta adecuada al cliente
    // Se registra en Program.cs
    // Funciona junto con la clase ApiException en API/Middleware/ApiException.cs

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; // Delegado para la siguiente pieza de middleware
        private readonly ILogger<ExceptionMiddleware> _logger; // Inyección del logger
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); // Registrar el error
                context.Response.ContentType = "application/json"; // Establecer el tipo de contenido de la respuesta
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Establecer el código de estado HTTP

                var response = _env.IsDevelopment() // Verificar si el entorno es de desarrollo 
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) // Proporcionar detalles del error en desarrollo
                    : new ApiErrorResponse(context.Response.StatusCode); // Mensaje genérico en producción

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // Configurar la serialización JSON
                var json = JsonSerializer.Serialize(response, options); // Serializar la respuesta de error a JSON
                await context.Response.WriteAsync(json); // Escribir la respuesta JSON en el cuerpo de la respuesta HTTP
            }
        }


    }
}

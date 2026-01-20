namespace API.Errores
{
    public class ApiException: ApiErrorResponse
    {

        // Clase para representar errores de la API
        // Contiene el código de estado HTTP, un mensaje y detalles del error
        // Se utiliza en el middleware de manejo de excepciones para devolver respuestas de error consistentes
        // Se encuentra en API/Middleware/ExceptionMiddleware.cs

        public ApiException(int statusCode, string mensaje=null, string detalle=null) :base(statusCode, mensaje)
        {
            Detalle = detalle;
        }
        
        public string Detalle { get; set; }
    }
}

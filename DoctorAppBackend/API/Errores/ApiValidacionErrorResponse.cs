namespace API.Errores
{
    public class ApiValidacionErrorResponse: ApiErrorResponse
    {
        public ApiValidacionErrorResponse() : base(400) // por que base es 400? 400 es el código de estado HTTP para solicitudes incorrectas
        {
        }
        public IEnumerable<string> Errores { get; set; } // Lista de mensajes de error de validación
    }
}

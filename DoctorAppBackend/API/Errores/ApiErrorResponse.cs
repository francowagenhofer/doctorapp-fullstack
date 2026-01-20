namespace API.Errores
{
    
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string mensaje=null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? GetMensajeStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Mensaje { get; set; }

        private string GetMensajeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Solicitud invalida",
                401 => "No autorizado",
                404 => "No encontrado",
                500 => "Error interno del servidor",
                //_ => "Error desconocido"
                _ => null
            };
        }   
    }
}

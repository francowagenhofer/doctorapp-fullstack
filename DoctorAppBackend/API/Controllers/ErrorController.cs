using API.Errores;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Controlador para manejar errores HTTP y devolver respuestas de error adecuadas

    [Route("errores/{codigo}")] // Ruta para manejar errores HTTP con un código específico
    [ApiExplorerSettings(IgnoreApi = true)] // Ignora este controlador en la documentación de la API
    public class ErrorController: BaseAPIController
    {
        public IActionResult Error(int codigo)  
        {
            return new ObjectResult(new ApiErrorResponse(codigo)); // Devuelve una respuesta de error con el código HTTP proporcionado
        }
    }
}

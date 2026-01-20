using API.Errores;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entidades;

namespace API.Controllers
{
    // Clase para probar los diferentes tipos de errores HTTP
    public class ErrorTestController: BaseAPIController
    {
        private readonly ApplicationDbContext _db;

        public ErrorTestController(ApplicationDbContext db)
        {
            _db = db;
        }

        //[Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetNotAuthorize()
        {
            // Este metodo sirve para probar el error 401 Unauthorized.
            // Aparece cuando el usuario no esta autenticado.
            return "No autorizado";   
        }

        //[Authorize]
        [HttpGet("not-found")]
        public ActionResult<Usuario> GetNotFound()
        {
            // Este metodo sirve para probar el error 404 Not Found.
            // Aparece cuando no se encuentra el recurso solicitado.
            var objeto = _db.Usuarios.Find(-1);
            
            if (objeto == null)
                return NotFound(new ApiErrorResponse(404));

            return objeto;
        }

        //[Authorize]
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            // Este metodo sirve para probar el error 500 Internal Server Error.
            // Aparece cuando hay un error en el servidor al procesar la solicitud.
            var objeto = _db.Usuarios.Find(-1);
            var objetoString = objeto.ToString(); 
            
            return objetoString; 
        }

        //[Authorize]
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            // Este metodo sirve para probar el error 400 Bad Request.
            // Aparece cuando la solicitud es invalida o malformada.
            return BadRequest(new ApiErrorResponse(400));
        }






    }
}

using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace API.Controllers
{
    [Route("api/[controller]")] // api/usuario - sirve para definir la ruta base del controlador
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _db; // Sirve para acceder a la base de datos

        public UsuarioController(ApplicationDbContext db) // Sirve para inyectar el contexto de la base de datos
        {
            _db = db;
        }

        [HttpGet] // api/usuario - Define que este método responde a solicitudes GET
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _db.Usuarios.ToListAsync(); // Obtener todos los usuarios de la base de datos
            return Ok(usuarios); // Retornar la lista de usuarios con un estado 200 OK
        }

        [HttpGet("{id}")] // api/usuario/{id} - Define que este método responde a solicitudes GET con un parámetro id
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id); // Buscar el usuario por id
        
            return Ok(usuario); // Retornar el usuario encontrado con un estado 200 OK
        }
    
    
    
    
    }
}

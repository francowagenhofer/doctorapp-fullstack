using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsuarioController : BaseAPIController
    {
        private readonly ApplicationDbContext _db; // Sirve para acceder a la base de datos
        private readonly ITokenServicio _tokenServicio; // Sirve para crear tokens JWT 

        // Cuando se agrega un servicio, siempre se aggreaga la interfaz y el contexto en el constructor

        public UsuarioController(ApplicationDbContext db, ITokenServicio tokenServicio) // Sirve para inyectar el contexto de la base de datos
        {
            _db = db;
            _tokenServicio = tokenServicio;
        }

        [Authorize]
        [HttpGet] // api/usuario - Sirve para obtener todos los usuarios
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _db.Usuarios.ToListAsync(); // Obtener todos los usuarios de la base de datos
            return Ok(usuarios); // Retornar la lista de usuarios con un estado 200 OK
        }

        [Authorize]
        [HttpGet("{id}")] // api/usuario/{id} - Sirve para obtener un usuario por su id
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id); // Buscar el usuario por id

            return Ok(usuario); // Retornar el usuario encontrado con un estado 200 OK
        }

        [HttpPost("registro")] // api/usuario/registro - Sirve para registrar un nuevo usuario
        public async Task<ActionResult<UsuarioDTO>> Registro(RegistroDTO registroDto)
        {
            if (await UsuarioExiste(registroDto.Username))
            {
                return BadRequest("El nombre de usuario ya existe"); // Retornar un estado 400 Bad Request si el usuario ya existe
            }

            using var  hmac = new HMACSHA512(); // Crear una instancia de HMACSHA512 para hashear la contraseña
            // HMACSHA512 sirve para crear un hash seguro de la contraseña utilizando una clave secreta (sal) 

            var usuario = new Usuario // Crea una nueva instancia de Usuario
            {
                Username = registroDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
                PasswordSalt = hmac.Key
            };

            _db.Usuarios.Add(usuario); // Agrega el nuevo usuario al contexto de la base de datos

            await _db.SaveChangesAsync(); // Guarda los cambios en la base de datos

            return new UsuarioDTO // Retornar un estado 200 OK con el usuario registrado
            {
                Username = usuario.Username, // Retorna el nombre de usuario
                Token = _tokenServicio.CrearToken(usuario) // Retorna el token JWT creado para el usuario
            };
        }


        [HttpPost("login")] // api/usuario/login - Sirve para autenticar a un usuario
        public async Task<ActionResult<UsuarioDTO>> Login(LoginDTO loginDto)
        {
            var usuario = await _db.Usuarios.SingleOrDefaultAsync(u => u.Username.ToLower() == loginDto.Username.ToLower());
            if (usuario == null)
                return Unauthorized("Usuario no valido"); // Retornar un estado 401 Unauthorized si el usuario no existe
          
            using var hmac = new HMACSHA512(usuario.PasswordSalt); // Crear una instancia de HMACSHA512 con la sal del usuario
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password)); // sirve para hashear la contraseña ingresada por el usuario
            
            for (int i = 0; i < computedHash.Length; i++) // Ciclo for para comparar el hash calculado con el hash almacenado en la base de datos
            {
                if (computedHash[i] != usuario.PasswordHash[i])
                    return Unauthorized("Contraseña incorrecta"); // Retornar un estado 401 Unauthorized si la contraseña es incorrecta
                
            }
            return new UsuarioDTO
            {
                Username = usuario.Username, 
                Token = _tokenServicio.CrearToken(usuario) // Retorna el token JWT creado para el usuario
            }; 

        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _db.Usuarios.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }



    }
}

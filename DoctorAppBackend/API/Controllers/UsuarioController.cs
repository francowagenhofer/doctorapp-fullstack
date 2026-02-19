using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{ 
    public class UsuarioController : BaseAPIController
    {
        //private readonly ApplicationDbContext _db; // Sirve para acceder a la base de datos
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ITokenServicio _tokenServicio; // Sirve para crear tokens JWT 
        private ApiResponse _response; 
        private readonly RoleManager<RolAplicacion> _rolManager;


        // Cuando se agrega un servicio, siempre se aggreaga la interfaz y el contexto en el constructor

        public UsuarioController(UserManager<UsuarioAplicacion> userManager, ITokenServicio tokenServicio,
            RoleManager<RolAplicacion> rolManager) // Sirve para inyectar el contexto de la base de datos
        {
            _userManager = userManager;
            _tokenServicio = tokenServicio;
            _response = new();
            _rolManager = rolManager;
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]  // api/usuario
        public async Task<ActionResult> GetUsuarios()
        {
            // Primero obtener todos los usuarios
            var usuarios = await _userManager.Users.ToListAsync();
            
            // Luego crear el DTO con los roles
            var usuariosLista = new List<UsuarioListaDTO>();
            
            foreach (var u in usuarios)
            {
                usuariosLista.Add(new UsuarioListaDTO
                {
                    Username = u.UserName,
                    Apellidos = u.Apellidos,
                    Nombres = u.Nombres,
                    Email = u.Email,
                    Rol = string.Join(",", await _userManager.GetRolesAsync(u))
                });
            }

            _response.Resultado = usuariosLista;
            _response.EsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        //[Authorize]
        //[HttpGet("{id}")] // api/usuario/{id} - Sirve para obtener un usuario por su id
        //public async Task<ActionResult<Usuario>> GetUsuario(int id)
        //{
        //    var usuario = await _db.Usuarios.FindAsync(id); // Buscar el usuario por id

        //    return Ok(usuario); // Retornar el usuario encontrado con un estado 200 OK
        //}

        [Authorize(Policy = "AdminRol")]
        [HttpPost("registro")] // api/usuario/registro - Sirve para registrar un nuevo usuario
        public async Task<ActionResult<UsuarioDTO>> Registro(RegistroDTO registroDto)
        {
            if (await UsuarioExiste(registroDto.Username))
            {
                return BadRequest("El nombre de usuario ya existe"); // Retornar un estado 400 Bad Request si el usuario ya existe
            }

            var usuario = new UsuarioAplicacion // Crea una nueva instancia de Usuario
            {
                UserName = registroDto.Username.ToLower(),
                Email = registroDto.Email,
                Nombres = registroDto.Nombres,
                Apellidos = registroDto.Apellidos
            };

            // Crea el usuario con Identity y hashea la contraseña
            var resultado = await _userManager.CreateAsync(usuario, registroDto.Password); 
            if(!resultado.Succeeded) return BadRequest(resultado.Errors); 

            // Asigna el rol al usuario
            var rolResultado = await _userManager.AddToRoleAsync(usuario, registroDto.Rol); 
            if(!rolResultado.Succeeded) return BadRequest(rolResultado.Errors);

            return new UsuarioDTO // Retornar un estado 200 OK con el usuario registrado
            {
                Username = usuario.UserName, // Retorna el nombre de usuario
                Token = await _tokenServicio.CrearToken(usuario) // Retorna el token JWT creado para el usuario
            };
        }


        [HttpPost("login")] // api/usuario/login - Sirve para autenticar a un usuario
        public async Task<ActionResult<UsuarioDTO>> Login(LoginDTO loginDto)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == loginDto.Username.ToLower());
            if (usuario == null) return Unauthorized("Usuario no valido"); // Retornar un estado 401 Unauthorized si el usuario no existe

            var resultado = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);
            if (!resultado) return Unauthorized("Contraseña incorrecta"); // Retornar un estado 401 Unauthorized si la contraseña es incorrecta

            return new UsuarioDTO
            {
                Username = usuario.UserName,
                Token = await _tokenServicio.CrearToken(usuario) // Retorna el token JWT creado para el usuario
            };

        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet("ListadoRoles")] // api/usuario/roles - Sirve para obtener todos los roles
        public IActionResult GetRoles()
        {
            var roles = _rolManager.Roles.Select(r => new { NombreRol = r.Name }).ToList();
            _response.Resultado = roles;
            _response.EsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        }



    }
}

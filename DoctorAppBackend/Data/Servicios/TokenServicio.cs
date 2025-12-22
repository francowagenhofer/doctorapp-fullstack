using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Servicios
{
    // El servicio de token se encarga de crear tokens JWT para la autenticación de usuarios 
    public class TokenServicio : ITokenServicio
    {
        private readonly SymmetricSecurityKey _key; // Esto sirve para almacenar la clave secreta utilizada para firmar el token

        public TokenServicio(IConfiguration config)
        {
            // La clave secreta se utiliza para firmar el token y debe ser segura
            // Esto sirve para obtener la clave secreta desde el archivo de configuración (appsettings.json)
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CrearToken(Usuario usuario) // Esto sirve para crear un token JWT para el usuario proporcionado
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.Username) 
                // esto sirve para agregar el nombre de usuario como un reclamo en el token
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); 
            // Esto sirve para crear las credenciales de firma utilizando la clave secreta y el algoritmo HMAC SHA512

            var tokenDescriptor = new SecurityTokenDescriptor  // Esto sirve para describir el token que se va a crear
            {
                Subject = new ClaimsIdentity(claims), // Esto sirve para establecer los reclamos del token
                Expires = DateTime.Now.AddDays(7), // Esto sirve para establecer la fecha de expiración del token (7 días en este caso)
                SigningCredentials = creds // Esto sirve para establecer las credenciales de firma del token
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); // Esto sirve para crear el token utilizando el descriptor

            return tokenHandler.WriteToken(token); // Esto sirve para retornar el token como una cadena de texto
        }
    }
}

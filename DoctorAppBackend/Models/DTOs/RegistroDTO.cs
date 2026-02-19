using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    // Los DTOs sirven para transferir datos entre el cliente y el servidor
    // Son objetos simples que no contienen lógica de negocio
    // Se utilizan para evitar exponer las entidades directamente
    public class RegistroDTO 
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La constraseña es requerida")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "La constraseña debe tener entre 4 y 10 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El apellido de usuario es requerido")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El email de usuario es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol de usuario es requerido")]
        public string Rol { get; set; }
    }

}

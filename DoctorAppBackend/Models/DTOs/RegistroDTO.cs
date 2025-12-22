using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RegistroDTO 
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La constraseña es requerida")]
        public string Password { get; set; }
    }
    // Los DTOs sirven para transferir datos entre el cliente y el servidor
    // son objetos simples que no contienen lógica de negocio
    // y se utilizan para evitar exponer las entidades directamente
}

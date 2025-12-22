using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; } // Almacena el hash de la contraseña
        public byte[] PasswordSalt { get; set; } // Almacena la sal utilizada para hashear la contraseña
    }
}

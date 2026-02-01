using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class EspecialidadDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "El nombre de la especialidad debe tener entre 1 y 60 caracteres.")]
        public string NombreEspecialidad { get; set; }

        [Required(ErrorMessage = "La descripcion es requerida")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "La descripción debe tener entre 1 y 200 caracteres.")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage ="El estado es requerido")]
        public int Estado { get; set; } // 1 - 0
    }
}

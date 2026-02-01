using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombres es Requerido")]
        [StringLength(60, MinimumLength =1, ErrorMessage = "Nombres debe ser Mínimo 1, Máximo 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage ="Apellidos es Requerido")]
        [StringLength(60, MinimumLength =1, ErrorMessage ="Apellidos debe ser Mínimo 1, Máximo 60 caracteres")]
        public string Apellidos { get; set; }

 
        [Required(ErrorMessage ="Dirección es Requerido")]
        [StringLength(100, MinimumLength =1, ErrorMessage = "Dirección debe ser Mínimo 1, Máximo 100 caracteres")]
        public string Direccion { get; set; }
        

        [MaxLength(40)]
        public string Telefono { get; set; }


        [Required(ErrorMessage = "Género es Requerido")]
        public char Genero { get; set; }

        [Required(ErrorMessage = "Especialidad es Requerido")]
        public int EspecialidadId { get; set; }

        [ForeignKey("EspecialidadId")]
        public Especialidad Especialidad { get; set; }

        public bool Estado { get; set; } // true - false

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}

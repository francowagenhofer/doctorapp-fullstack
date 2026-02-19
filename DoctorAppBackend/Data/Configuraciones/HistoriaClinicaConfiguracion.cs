using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuraciones
{
    // Clase de configuración para la entidad Especialidad
    // Sirve para definir las reglas y restricciones de la entidad en la base de datos
    // Implementa la interfaz IEntityTypeConfiguration<T> para configurar la entidad Especialidad 
    public class HistoriaClinicaConfiguracion : IEntityTypeConfiguration<HistoriaClinica>
    {
        public void Configure(EntityTypeBuilder<HistoriaClinica> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.PacienteId).IsRequired();

            /* Relaciones */

            builder.HasOne(x => x.Paciente).WithOne(x => x.HistoriaClinica)
                   .HasForeignKey<HistoriaClinica>(x => x.PacienteId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

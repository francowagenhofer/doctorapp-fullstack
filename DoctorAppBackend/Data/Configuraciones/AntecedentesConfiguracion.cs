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
    public class AntecedentesConfiguracion : IEntityTypeConfiguration<Antecedente>
    {
        public void Configure(EntityTypeBuilder<Antecedente> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.HistoriaClinicaId).IsRequired();

            /* Relaciones */

            builder.HasOne(x => x.HistoriaClinica).WithMany()
                   .HasForeignKey(x => x.HistoriaClinicaId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

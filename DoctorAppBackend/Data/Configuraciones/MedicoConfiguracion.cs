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
    public class MedicoConfiguracion : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Nombres).IsRequired().HasMaxLength(60);
            builder.Property(e => e.Apellidos).IsRequired().HasMaxLength(60);
            builder.Property(e => e.Direccion).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Telefono).IsRequired(false).HasMaxLength(40);
            builder.Property(e => e.Genero).IsRequired().HasColumnType("char").HasMaxLength(1);
            builder.Property(e => e.Estado).IsRequired();
            builder.Property(e => e.EspecialidadId).IsRequired();

            //RELACIONES

            builder.HasOne(x => x.Especialidad).WithMany(). // Relación uno a muchos
                HasForeignKey(e => e.EspecialidadId). // Llave foránea
                OnDelete(DeleteBehavior.NoAction); // Evita la eliminación en cascada

        }
    }
}

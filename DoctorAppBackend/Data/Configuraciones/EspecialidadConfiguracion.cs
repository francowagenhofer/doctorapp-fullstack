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
    public class EspecialidadConfiguracion : IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.NombreEspecialidad).IsRequired().HasMaxLength(60);
            builder.Property(e => e.Descripcion).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Estado).IsRequired();
        }
    }
}

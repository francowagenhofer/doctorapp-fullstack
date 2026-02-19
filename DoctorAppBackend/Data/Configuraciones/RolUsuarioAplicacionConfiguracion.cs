using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;

namespace Data.Configuraciones
{
    public class RolUsuarioAplicacionConfiguracion : IEntityTypeConfiguration<RolUsuarioAplicacion>
    {
        public void Configure(EntityTypeBuilder<RolUsuarioAplicacion> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(x => x.UsuarioAplicacion)
                .WithMany(u => u.RolUsuarios)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.RolAplicacion)
                .WithMany(r => r.RolUsuarios)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            builder.ToTable("AspNetUserRoles");
        }
    }
}
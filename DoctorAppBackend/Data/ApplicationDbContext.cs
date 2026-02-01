using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }   
        public DbSet<Especialidad> Especialidades { get; set; }

        public DbSet<Medico> Medicos { get; set; }

        // Configuración del modelo de datos. Aquí se pueden definir las relaciones, restricciones y otras configuraciones de las entidades.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llama al método base para asegurar que las configuraciones predeterminadas se apliquen
            base.OnModelCreating(modelBuilder);

            // Aplica todas las configuraciones de entidad en el ensamblado actual
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



        }


    }
}

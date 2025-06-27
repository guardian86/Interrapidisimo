using Microsoft.EntityFrameworkCore;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Infrastructure.Configurations;

namespace Interrapidisimo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<MateriaProfesor> MateriaProfesor { get; set; }
        public DbSet<EstudianteMateriaProfesor> EstudianteMateriaProfesor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas las configuraciones de entidades
            modelBuilder.ApplyConfiguration(new EstudianteConfiguration());
            modelBuilder.ApplyConfiguration(new ProfesorConfiguration());
            modelBuilder.ApplyConfiguration(new MateriaConfiguration());
            modelBuilder.ApplyConfiguration(new MateriaProfesorConfiguration());
            modelBuilder.ApplyConfiguration(new EstudianteMateriaProfesorConfiguration());
        }

    }
}

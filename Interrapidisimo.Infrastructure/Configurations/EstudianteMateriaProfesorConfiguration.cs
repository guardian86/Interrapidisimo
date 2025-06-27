using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Infrastructure.Configurations
{
    public class EstudianteMateriaProfesorConfiguration : IEntityTypeConfiguration<EstudianteMateriaProfesor>
    {
        public void Configure(EntityTypeBuilder<EstudianteMateriaProfesor> builder)
        {
            builder.ToTable("EstudiantesMateriasProfesores");

            builder.HasKey(emp => emp.Id);

            builder.Property(emp => emp.EstudianteId)
                .IsRequired();

            builder.Property(emp => emp.MateriaId)
                .IsRequired();

            builder.Property(emp => emp.ProfesorId)
                .IsRequired();

            builder.Property(emp => emp.FechaInscripcion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(emp => emp.Estado)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Activo");

            builder.Property(emp => emp.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(emp => emp.UpdatedAt);

            builder.Property(emp => emp.IsActive)
                .HasDefaultValue(true);

            // Índice único compuesto para evitar inscripciones duplicadas
            builder.HasIndex(emp => new { emp.EstudianteId, emp.MateriaId, emp.ProfesorId }).IsUnique();

            // Relaciones
            builder.HasOne(emp => emp.Estudiante)
                .WithMany(e => e.EstudianteMateriaProfesor)
                .HasForeignKey(emp => emp.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(emp => emp.Materia)
                .WithMany(m => m.EstudianteMateriaProfesor)
                .HasForeignKey(emp => emp.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(emp => emp.Profesor)
                .WithMany(p => p.EstudianteMateriaProfesor)
                .HasForeignKey(emp => emp.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

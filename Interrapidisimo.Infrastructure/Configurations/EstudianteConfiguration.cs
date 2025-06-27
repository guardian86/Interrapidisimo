using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Infrastructure.Configurations
{
    public class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {
            builder.ToTable("Estudiantes");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Documento)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.FechaNacimiento)
                .IsRequired();

            builder.Property(e => e.CreditosSeleccionados)
                .HasDefaultValue(0);

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt);

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            // Índices únicos
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.Documento).IsUnique();

            // Relaciones
            builder.HasMany(e => e.EstudianteMateriaProfesor)
                .WithOne(emp => emp.Estudiante)
                .HasForeignKey(emp => emp.EstudianteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

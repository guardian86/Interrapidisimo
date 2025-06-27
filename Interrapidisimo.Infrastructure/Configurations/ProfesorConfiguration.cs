using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Infrastructure.Configurations
{
    public class ProfesorConfiguration : IEntityTypeConfiguration<Profesor>
    {
        public void Configure(EntityTypeBuilder<Profesor> builder)
        {
            builder.ToTable("Profesores");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Telefono)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Especialidad)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.UpdatedAt);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            // Índice único
            builder.HasIndex(p => p.Email).IsUnique();

            // Relaciones
            builder.HasMany(p => p.MateriaProfesor)
                .WithOne(mp => mp.Profesor)
                .HasForeignKey(mp => mp.ProfesorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

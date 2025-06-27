using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Infrastructure.Configurations
{
    public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.ToTable("Materias");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(m => m.Codigo)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(m => m.Descripcion)
                .HasMaxLength(500);

            builder.Property(m => m.Creditos)
                .IsRequired()
                .HasDefaultValue(3);

            builder.Property(m => m.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(m => m.UpdatedAt);

            builder.Property(m => m.IsActive)
                .HasDefaultValue(true);

            // Índices únicos
            builder.HasIndex(m => m.Codigo).IsUnique();
            builder.HasIndex(m => m.Nombre).IsUnique();

            // Relaciones
            builder.HasMany(m => m.MateriaProfesor)
                .WithOne(mp => mp.Materia)
                .HasForeignKey(mp => mp.MateriaId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}

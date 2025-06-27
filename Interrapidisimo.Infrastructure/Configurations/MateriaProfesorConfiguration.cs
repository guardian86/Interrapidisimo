using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Infrastructure.Configurations
{
    public class MateriaProfesorConfiguration : IEntityTypeConfiguration<MateriaProfesor>
    {
        public void Configure(EntityTypeBuilder<MateriaProfesor> builder)
        {
            builder.ToTable("MateriasProfesores");

            builder.HasKey(mp => mp.Id);

            builder.Property(mp => mp.MateriaId)
                .IsRequired();

            builder.Property(mp => mp.ProfesorId)
                .IsRequired();

            builder.Property(mp => mp.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(mp => mp.UpdatedAt);

            builder.Property(mp => mp.IsActive)
                .HasDefaultValue(true);

            // Índice único compuesto para evitar duplicados
            builder.HasIndex(mp => new { mp.MateriaId, mp.ProfesorId }).IsUnique();

            // Relaciones
            builder.HasOne(mp => mp.Materia)
                .WithMany(m => m.MateriaProfesor)
                .HasForeignKey(mp => mp.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mp => mp.Profesor)
                .WithMany(p => p.MateriaProfesor)
                .HasForeignKey(mp => mp.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

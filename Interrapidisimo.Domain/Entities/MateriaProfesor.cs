using Interrapidisimo.Domain.Common;

namespace Interrapidisimo.Domain.Entities
{
    // Entidad de relación entre Materia y Profesor (Many-to-Many)
    public class MateriaProfesor : BaseEntity
    {
        public int MateriaId { get; set; }
        public virtual Materia Materia { get; set; } = null!;
        
        public int ProfesorId { get; set; }
        public virtual Profesor Profesor { get; set; } = null!;
        
        // Relaciones con estudiantes matriculados en esta combinación materia-profesor
        public virtual ICollection<EstudianteMateriaProfesor> EstudiantesMatriculados { get; set; } = new List<EstudianteMateriaProfesor>();
    }
}

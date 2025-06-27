using Interrapidisimo.Domain.Common;

namespace Interrapidisimo.Domain.Entities
{
    // Entidad de relación entre Estudiante, Materia y Profesor (Many-to-Many-to-Many)
    public class EstudianteMateriaProfesor : BaseEntity
    {
        public int EstudianteId { get; set; }
        public virtual Estudiante Estudiante { get; set; } = null!;
        
        public int MateriaId { get; set; }
        public virtual Materia Materia { get; set; } = null!;
        
        public int ProfesorId { get; set; }
        public virtual Profesor Profesor { get; set; } = null!;
        
        public DateTime FechaInscripcion { get; set; }
        public string Estado { get; set; } = "Activo"; // Activo, Completado, Cancelado
    }
}

using Interrapidisimo.Domain.Common;

namespace Interrapidisimo.Domain.Entities
{
    public class Profesor : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        
        // Relaciones - Cada profesor dicta exactamente 2 materias
        public virtual ICollection<MateriaProfesor> MateriasQueDicta { get; set; } = new List<MateriaProfesor>();
    }
}

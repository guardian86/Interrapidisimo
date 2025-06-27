using Interrapidisimo.Domain.Common;

namespace Interrapidisimo.Domain.Entities
{
    public class Estudiante : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Documento { get; set; } = string.Empty;
        public int CreditosSeleccionados { get; set; } = 0;
        
        // Relaciones
        public virtual ICollection<EstudianteMateriaProfesor> MateriasInscritas { get; set; } = new List<EstudianteMateriaProfesor>();
    }
}

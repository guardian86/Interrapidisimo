using Interrapidisimo.Domain.Common;

namespace Interrapidisimo.Domain.Entities
{
    public class Materia : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Creditos { get; set; } = 3; // Cada materia equivale a 3 créditos
        
        // Relaciones
        public virtual ICollection<MateriaProfesor> MateriaProfesor { get; set; } = new List<MateriaProfesor>();
        public virtual ICollection<EstudianteMateriaProfesor> EstudianteMateriaProfesor { get; set; } = new List<EstudianteMateriaProfesor>();
    }
}

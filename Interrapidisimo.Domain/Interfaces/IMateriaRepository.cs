using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IMateriaRepository : IGenericRepository<Materia>
    {
        Task<Materia?> GetMateriaWithProfesoresAsync(int materiaId);
        Task<IEnumerable<Materia>> GetMateriasDisponiblesAsync();
        Task<IEnumerable<Materia>> GetMateriasByProfesorAsync(int profesorId);
    }
}

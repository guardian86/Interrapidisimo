using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IEstudianteRepository : IGenericRepository<Estudiante>
    {
        Task<Estudiante?> GetEstudianteWithMateriasAsync(int estudianteId);
        Task<IEnumerable<Estudiante>> GetEstudiantesConMateriasCompartidasAsync(int estudianteId, int materiaId);
        Task<bool> TieneClaseConProfesorAsync(int estudianteId, int profesorId);
        Task<int> GetCreditosSeleccionadosAsync(int estudianteId);
        Task<IEnumerable<Estudiante>> GetEstudiantesPorMateriaAsync(int materiaId);
    }
}

using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IEstudianteMateriaProfesorRepository : IGenericRepository<EstudianteMateriaProfesor>
    {
        Task<IEnumerable<EstudianteMateriaProfesor>> GetInscripcionesPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<EstudianteMateriaProfesor>> GetEstudiantesPorMateriaProfesorAsync(int materiaProfesorId);
        Task<bool> EstudianteTieneClaseConProfesorAsync(int estudianteId, int profesorId);
        Task<int> ContarMateriasDelEstudianteAsync(int estudianteId);
    }
}

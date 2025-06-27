using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<Estudiante?> GetByIdAsync(int id);
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante> AddAsync(Estudiante estudiante);
        void Update(Estudiante estudiante);
        void Delete(Estudiante estudiante);
        Task<bool> ExistsAsync(int id);
        Task<Estudiante?> GetEstudianteWithMateriasAsync(int estudianteId);
        Task<IEnumerable<Estudiante>> GetCompanerosDeClaseAsync(int estudianteId, int materiaId);
        Task<bool> TieneClaseConProfesorAsync(int estudianteId, int profesorId);
        Task<int> GetCreditosSeleccionadosAsync(int estudianteId);
        Task<IEnumerable<Estudiante>> GetEstudiantesPorMateriaAsync(int materiaId);
    }
}

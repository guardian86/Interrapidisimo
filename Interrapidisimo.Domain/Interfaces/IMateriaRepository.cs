using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IMateriaRepository
    {
        Task<Materia?> GetByIdAsync(int id);
        Task<IEnumerable<Materia>> GetAllAsync();
        Task<Materia> AddAsync(Materia materia);
        void Update(Materia materia);
        void Delete(Materia materia);
        Task<bool> ExistsAsync(int id);
        Task<Materia?> GetMateriaWithProfesoresAsync(int materiaId);
        Task<IEnumerable<Materia>> GetMateriasDisponiblesAsync();
        Task<IEnumerable<Materia>> GetMateriasByProfesorAsync(int profesorId);
        Task<IEnumerable<Materia>> GetMateriasDisponiblesParaEstudianteAsync(int estudianteId);
    }
}

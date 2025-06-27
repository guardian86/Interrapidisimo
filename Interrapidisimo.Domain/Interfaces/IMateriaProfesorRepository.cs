using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IMateriaProfesorRepository
    {
        Task<MateriaProfesor?> GetMateriaProfesorAsync(int materiaId, int profesorId);
        Task<MateriaProfesor> AddAsync(MateriaProfesor materiaProfesor);
        void Delete(MateriaProfesor materiaProfesor);
        Task<IEnumerable<MateriaProfesor>> GetMateriasPorProfesorAsync(int profesorId);
        Task<IEnumerable<MateriaProfesor>> GetProfesoresPorMateriaAsync(int materiaId);
        Task<bool> ExistsAsync(int materiaId, int profesorId);
    }
}

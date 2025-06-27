using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IMateriaProfesorRepository : IGenericRepository<MateriaProfesor>
    {
        Task<MateriaProfesor?> GetMateriaProfesorWithDetailsAsync(int materiaId, int profesorId);
        Task<IEnumerable<MateriaProfesor>> GetMateriasPorProfesorAsync(int profesorId);
        Task<IEnumerable<MateriaProfesor>> GetProfesoresPorMateriaAsync(int materiaId);
    }
}

using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IProfesorRepository : IGenericRepository<Profesor>
    {
        Task<Profesor?> GetProfesorWithMateriasAsync(int profesorId);
        Task<IEnumerable<Profesor>> GetProfesoresConMenosDeDosMateriasAsync();
        Task<bool> DictaExactamenteDosMateriasAsync(int profesorId);
    }
}

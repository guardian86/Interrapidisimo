using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IProfesorRepository
    {
        Task<Profesor?> GetByIdAsync(int id);
        Task<IEnumerable<Profesor>> GetAllAsync();
        Task<Profesor> AddAsync(Profesor profesor);
        void Update(Profesor profesor);
        void Delete(Profesor profesor);
        Task<bool> ExistsAsync(int id);
        Task<Profesor?> GetProfesorWithMateriasAsync(int profesorId);
        Task<IEnumerable<Profesor>> GetProfesoresConMenosDeDosMateriasAsync();
        Task<bool> DictaExactamenteDosMateriasAsync(int profesorId);
    }
}

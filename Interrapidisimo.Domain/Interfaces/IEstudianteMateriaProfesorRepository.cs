using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Domain.Interfaces
{
    public interface IEstudianteMateriaProfesorRepository
    {
        Task<EstudianteMateriaProfesor> AddAsync(EstudianteMateriaProfesor inscripcion);
        void Delete(EstudianteMateriaProfesor inscripcion);
        Task<EstudianteMateriaProfesor?> GetInscripcionAsync(int estudianteId, int materiaId, int profesorId);
        Task<bool> ExisteInscripcionAsync(int estudianteId, int materiaId, int profesorId);
        Task<IEnumerable<EstudianteMateriaProfesor>> GetMateriasDelEstudianteAsync(int estudianteId);
        Task<IEnumerable<EstudianteMateriaProfesor>> GetEstudiantesPorMateriaProfesorAsync(int materiaId, int profesorId);
        Task<bool> EstudianteTieneClaseConProfesorAsync(int estudianteId, int profesorId);
        Task<int> ContarMateriasDelEstudianteAsync(int estudianteId);
        Task<IEnumerable<EstudianteMateriaProfesor>> GetMateriasPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<EstudianteMateriaProfesor>> GetProfesoresPorEstudianteAsync(int estudianteId);
    }
}

using Interrapidisimo.Application.DTOs;

namespace Interrapidisimo.Application.Interfaces
{
    public interface IInscripcionService
    {
        Task<InscripcionResponseDto> InscribirEstudianteAsync(InscripcionCreateDto inscripcionDto);
        Task<bool> DesinscribirEstudianteAsync(int estudianteId, int materiaId, int profesorId);
        Task<IEnumerable<MateriaInscritaDto>> GetMateriasDelEstudianteAsync(int estudianteId);
        Task<bool> ValidarInscripcionAsync(int estudianteId, int materiaId, int profesorId);
        Task<bool> EstudiantePuedeInscribirseAsync(int estudianteId);
    }
}

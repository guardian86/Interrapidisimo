using Interrapidisimo.Application.DTOs;

namespace Interrapidisimo.Application.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteListDto>> GetAllAsync();
        Task<EstudianteDto?> GetByIdAsync(int id);
        Task<EstudianteDto> CreateAsync(EstudianteCreateDto estudianteCreateDto);
        Task<EstudianteDto> UpdateAsync(int id, EstudianteUpdateDto estudianteUpdateDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EstudianteCompaneroDto>> GetCompanerosAsync(int estudianteId, int materiaId);
        Task<bool> ExisteEstudianteAsync(int id);
    }
}

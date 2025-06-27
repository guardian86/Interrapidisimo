using Interrapidisimo.Application.DTOs;

namespace Interrapidisimo.Application.Interfaces
{
    public interface IMateriaService
    {
        Task<IEnumerable<MateriaListDto>> GetAllMateriasAsync();
        Task<MateriaDto?> GetMateriaByIdAsync(int id);
        Task<MateriaDto> CreateMateriaAsync(MateriaCreateDto materiaCreateDto);
        Task<MateriaDto> UpdateMateriaAsync(int id, MateriaUpdateDto materiaUpdateDto);
        Task<bool> DeleteMateriaAsync(int id);
        Task<IEnumerable<MateriasDisponiblesParaEstudianteDto>> GetMateriasDisponiblesParaEstudianteAsync(int estudianteId);
        Task<bool> ExisteMateriaAsync(int id);
    }
}

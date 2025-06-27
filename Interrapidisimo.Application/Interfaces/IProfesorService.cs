using Interrapidisimo.Application.DTOs;

namespace Interrapidisimo.Application.Interfaces
{
    public interface IProfesorService
    {
        Task<IEnumerable<ProfesorListDto>> GetAllProfesoresAsync();
        Task<ProfesorDto?> GetProfesorByIdAsync(int id);
        Task<ProfesorDto> CreateProfesorAsync(ProfesorCreateDto profesorCreateDto);
        Task<ProfesorDto> UpdateProfesorAsync(int id, ProfesorUpdateDto profesorUpdateDto);
        Task<bool> DeleteProfesorAsync(int id);
        Task<bool> ExisteProfesorAsync(int id);
    }
}

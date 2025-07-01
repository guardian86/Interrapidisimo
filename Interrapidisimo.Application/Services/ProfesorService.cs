using AutoMapper;
using Interrapidisimo.Application.DTOs;
using Interrapidisimo.Application.Interfaces;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;

namespace Interrapidisimo.Application.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfesorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfesorListDto>> GetAllProfesoresAsync()
        {
            var profesores = await _unitOfWork.ProfesorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProfesorListDto>>(profesores);
        }

        public async Task<ProfesorDto?> GetProfesorByIdAsync(int id)
        {
            var profesor = await _unitOfWork.ProfesorRepository.GetByIdAsync(id);
            return profesor != null ? _mapper.Map<ProfesorDto>(profesor) : null;
        }

        public async Task<ProfesorDto> CreateProfesorAsync(ProfesorCreateDto profesorCreateDto)
        {
            var profesor = _mapper.Map<Profesor>(profesorCreateDto);
            await _unitOfWork.ProfesorRepository.AddAsync(profesor);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProfesorDto>(profesor);
        }

        public async Task<ProfesorDto> UpdateProfesorAsync(int id, ProfesorUpdateDto profesorUpdateDto)
        {
            var profesor = await _unitOfWork.ProfesorRepository.GetByIdAsync(id);
            if (profesor == null)
                throw new KeyNotFoundException($"Profesor con Id {id} no encontrado");

            _mapper.Map(profesorUpdateDto, profesor);
            _unitOfWork.ProfesorRepository.Update(profesor);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProfesorDto>(profesor);
        }

        public async Task<bool> DeleteProfesorAsync(int id)
        {
            var profesor = await _unitOfWork.ProfesorRepository.GetByIdAsync(id);
            if (profesor == null)
                return false;

            _unitOfWork.ProfesorRepository.Delete(profesor);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteProfesorAsync(int id)
        {
            return await _unitOfWork.ProfesorRepository.ExistsAsync(id);
        }
    }
}

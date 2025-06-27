using AutoMapper;
using Interrapidisimo.Application.DTOs;
using Interrapidisimo.Application.Interfaces;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;

namespace Interrapidisimo.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstudianteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstudianteListDto>> GetAllAsync()
        {
            var estudiantes = await _unitOfWork.EstudianteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EstudianteListDto>>(estudiantes);
        }

        public async Task<EstudianteDto?> GetByIdAsync(int id)
        {
            var estudiante = await _unitOfWork.EstudianteRepository.GetByIdAsync(id);
            return estudiante != null ? _mapper.Map<EstudianteDto>(estudiante) : null;
        }

        public async Task<EstudianteDto> CreateAsync(EstudianteCreateDto estudianteCreateDto)
        {
            var estudiante = _mapper.Map<Estudiante>(estudianteCreateDto);
            await _unitOfWork.EstudianteRepository.AddAsync(estudiante);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<EstudianteDto>(estudiante);
        }

        public async Task<EstudianteDto> UpdateAsync(int id, EstudianteUpdateDto estudianteUpdateDto)
        {
            var estudiante = await _unitOfWork.EstudianteRepository.GetByIdAsync(id);
            if (estudiante == null)
                throw new KeyNotFoundException($"Estudiante with ID {id} not found");

            _mapper.Map(estudianteUpdateDto, estudiante);
            _unitOfWork.EstudianteRepository.Update(estudiante);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<EstudianteDto>(estudiante);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estudiante = await _unitOfWork.EstudianteRepository.GetByIdAsync(id);
            if (estudiante == null)
                return false;

            _unitOfWork.EstudianteRepository.Delete(estudiante);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EstudianteCompaneroDto>> GetCompanerosAsync(int estudianteId, int materiaId)
        {
            var companeros = await _unitOfWork.EstudianteRepository.GetCompanerosDeClaseAsync(estudianteId, materiaId);
            return _mapper.Map<IEnumerable<EstudianteCompaneroDto>>(companeros);
        }

        public async Task<bool> ExisteEstudianteAsync(int id)
        {
            return await _unitOfWork.EstudianteRepository.ExistsAsync(id);
        }
    }
}

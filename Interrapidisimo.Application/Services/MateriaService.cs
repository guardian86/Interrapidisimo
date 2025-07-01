using AutoMapper;
using Interrapidisimo.Application.DTOs;
using Interrapidisimo.Application.Interfaces;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;

namespace Interrapidisimo.Application.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MateriaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MateriaListDto>> GetAllMateriasAsync()
        {
            var materias = await _unitOfWork.MateriaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MateriaListDto>>(materias);
        }

        public async Task<MateriaDto?> GetMateriaByIdAsync(int id)
        {
            var materia = await _unitOfWork.MateriaRepository.GetByIdAsync(id);
            return materia != null ? _mapper.Map<MateriaDto>(materia) : null;
        }

        public async Task<MateriaDto> CreateMateriaAsync(MateriaCreateDto materiaCreateDto)
        {
            var materia = _mapper.Map<Materia>(materiaCreateDto);
            await _unitOfWork.MateriaRepository.AddAsync(materia);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<MateriaDto>(materia);
        }

        public async Task<MateriaDto> UpdateMateriaAsync(int id, MateriaUpdateDto materiaUpdateDto)
        {
            var materia = await _unitOfWork.MateriaRepository.GetByIdAsync(id);
            if (materia == null)
                throw new KeyNotFoundException($"Materia con id {id} no encontrada");

            _mapper.Map(materiaUpdateDto, materia);
            _unitOfWork.MateriaRepository.Update(materia);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<MateriaDto>(materia);
        }

        public async Task<bool> DeleteMateriaAsync(int id)
        {
            var materia = await _unitOfWork.MateriaRepository.GetByIdAsync(id);
            if (materia == null)
                return false;

            _unitOfWork.MateriaRepository.Delete(materia);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MateriasDisponiblesParaEstudianteDto>> GetMateriasDisponiblesParaEstudianteAsync(int estudianteId)
        {
            var materias = await _unitOfWork.MateriaRepository.GetMateriasDisponiblesParaEstudianteAsync(estudianteId);
            var materiasDto = new List<MateriasDisponiblesParaEstudianteDto>();

            foreach (var materia in materias)
            {
                var materiaDto = _mapper.Map<MateriasDisponiblesParaEstudianteDto>(materia);
                
                // Obtener profesores disponibles para esta materia
                var materiaProfesores = await _unitOfWork.MateriaProfesorRepository.GetProfesoresPorMateriaAsync(materia.Id);
                var profesores = materiaProfesores.Select(mp => mp.Profesor).ToList();
                materiaDto.ProfesoresDisponibles = _mapper.Map<List<ProfesorDisponibleDto>>(profesores);
                
                materiasDto.Add(materiaDto);
            }

            return materiasDto;
        }

        public async Task<bool> ExisteMateriaAsync(int id)
        {
            return await _unitOfWork.MateriaRepository.ExistsAsync(id);
        }
    }
}

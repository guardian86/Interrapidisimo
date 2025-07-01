using AutoMapper;
using Interrapidisimo.Application.DTOs;
using Interrapidisimo.Application.Interfaces;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;

namespace Interrapidisimo.Application.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InscripcionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InscripcionResponseDto> InscribirEstudianteAsync(InscripcionCreateDto inscripcionDto)
        {
            // Validar que el estudiante, materia y profesor existen
            var estudiante = await _unitOfWork.EstudianteRepository.GetByIdAsync(inscripcionDto.EstudianteId);
            if (estudiante == null)
                throw new KeyNotFoundException($"Estudiante con Id {inscripcionDto.EstudianteId} no encontrado");

            var materia = await _unitOfWork.MateriaRepository.GetByIdAsync(inscripcionDto.MateriaId);
            if (materia == null)
                throw new KeyNotFoundException($"Materia con Id {inscripcionDto.MateriaId} no encontrado");

            var profesor = await _unitOfWork.ProfesorRepository.GetByIdAsync(inscripcionDto.ProfesorId);
            if (profesor == null)
                throw new KeyNotFoundException($"Profesor con Id {inscripcionDto.ProfesorId} no encontrado");

            // Validar que la relación materia-profesor existe
            var materiaProfesor = await _unitOfWork.MateriaProfesorRepository
                .GetMateriaProfesorAsync(inscripcionDto.MateriaId, inscripcionDto.ProfesorId);
            if (materiaProfesor == null)
                throw new InvalidOperationException("El profesor especificado no enseña esta materia.");

            // VALIDACIÓN: Máximo 3 materias por estudiante
            var materiasActuales = await _unitOfWork.EstudianteMateriaProfesorRepository
                .GetMateriasPorEstudianteAsync(inscripcionDto.EstudianteId);
            if (materiasActuales.Count() >= 3)
                throw new InvalidOperationException("El estudiante no puede inscribirse en más de 3 materias");

            // VALIDACIÓN: No mismo profesor en diferentes materias
            var profesoresDelEstudiante = await _unitOfWork.EstudianteMateriaProfesorRepository
                .GetProfesoresPorEstudianteAsync(inscripcionDto.EstudianteId);
            if (profesoresDelEstudiante.Any(p => p.ProfesorId == inscripcionDto.ProfesorId))
                throw new InvalidOperationException("El estudiante no puede tener clases con el mismo profesor en diferentes asignaturas.");

            // Verificar si ya está inscrito
            var existeInscripcion = await _unitOfWork.EstudianteMateriaProfesorRepository
                .ExisteInscripcionAsync(inscripcionDto.EstudianteId, inscripcionDto.MateriaId, inscripcionDto.ProfesorId);
            if (existeInscripcion)
                throw new InvalidOperationException("El estudiante ya está inscrito en esta asignatura con este profesor.");

            // Crear la inscripción
            var inscripcion = new EstudianteMateriaProfesor
            {
                EstudianteId = inscripcionDto.EstudianteId,
                MateriaId = inscripcionDto.MateriaId,
                ProfesorId = inscripcionDto.ProfesorId,
                FechaInscripcion = DateTime.UtcNow
            };

            await _unitOfWork.EstudianteMateriaProfesorRepository.AddAsync(inscripcion);
            await _unitOfWork.SaveChangesAsync();

            return new InscripcionResponseDto
            {
                EstudianteId = inscripcion.EstudianteId,
                MateriaId = inscripcion.MateriaId,
                ProfesorId = inscripcion.ProfesorId,
                FechaInscripcion = inscripcion.FechaInscripcion,
                Exitoso = true,
                Mensaje = "El estudiante se inscribió con éxito"
            };
        }

        public async Task<bool> DesinscribirEstudianteAsync(int estudianteId, int materiaId, int profesorId)
        {
            var inscripcion = await _unitOfWork.EstudianteMateriaProfesorRepository
                .GetInscripcionAsync(estudianteId, materiaId, profesorId);
            
            if (inscripcion == null)
                return false;

            _unitOfWork.EstudianteMateriaProfesorRepository.Delete(inscripcion);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MateriaInscritaDto>> GetMateriasDelEstudianteAsync(int estudianteId)
        {
            var inscripciones = await _unitOfWork.EstudianteMateriaProfesorRepository
                .GetMateriasDelEstudianteAsync(estudianteId);
            return _mapper.Map<IEnumerable<MateriaInscritaDto>>(inscripciones);
        }

        public async Task<bool> ValidarInscripcionAsync(int estudianteId, int materiaId, int profesorId)
        {
            // Verificar que el estudiante existe
            if (!await _unitOfWork.EstudianteRepository.ExistsAsync(estudianteId))
                return false;

            // Verificar que la materia existe
            if (!await _unitOfWork.MateriaRepository.ExistsAsync(materiaId))
                return false;

            // Verificar que el profesor existe
            if (!await _unitOfWork.ProfesorRepository.ExistsAsync(profesorId))
                return false;

            // Verificar que la relación materia-profesor existe
            var materiaProfesor = await _unitOfWork.MateriaProfesorRepository
                .GetMateriaProfesorAsync(materiaId, profesorId);
            if (materiaProfesor == null)
                return false;

            // Verificar que no esté ya inscrito
            var existeInscripcion = await _unitOfWork.EstudianteMateriaProfesorRepository
                .ExisteInscripcionAsync(estudianteId, materiaId, profesorId);
            
            return !existeInscripcion;
        }


        //reglas de negocio inscripcion
        public async Task<bool> EstudiantePuedeInscribirseAsync(int estudianteId)
        {
            // Verificar que el estudiante existe
            if (!await _unitOfWork.EstudianteRepository.ExistsAsync(estudianteId))
                return false;



            return true;
        }
    }
}

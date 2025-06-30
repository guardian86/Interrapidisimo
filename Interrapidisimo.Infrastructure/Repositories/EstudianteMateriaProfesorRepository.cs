using Microsoft.EntityFrameworkCore;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;
using Interrapidisimo.Infrastructure.Data;

namespace Interrapidisimo.Infrastructure.Repositories
{
    public class EstudianteMateriaProfesorRepository : IEstudianteMateriaProfesorRepository
    {
        private readonly ApplicationDbContext _context;

        public EstudianteMateriaProfesorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EstudianteMateriaProfesor> AddAsync(EstudianteMateriaProfesor inscripcion)
        {
            await _context.EstudianteMateriaProfesor.AddAsync(inscripcion);
            return inscripcion;
        }

        public void Delete(EstudianteMateriaProfesor inscripcion)
        {
            _context.EstudianteMateriaProfesor.Remove(inscripcion);
        }

        public async Task<EstudianteMateriaProfesor?> GetInscripcionAsync(int estudianteId, int materiaId, int profesorId)
        {
            return await _context.EstudianteMateriaProfesor
                .Include(emp => emp.Estudiante)
                .Include(emp => emp.Materia)
                .Include(emp => emp.Profesor)
                .FirstOrDefaultAsync(emp => emp.EstudianteId == estudianteId && 
                                           emp.MateriaId == materiaId && 
                                           emp.ProfesorId == profesorId);
        }

        public async Task<bool> ExisteInscripcionAsync(int estudianteId, int materiaId, int profesorId)
        {
            return await _context.EstudianteMateriaProfesor
                .AnyAsync(emp => emp.EstudianteId == estudianteId && 
                                emp.MateriaId == materiaId && 
                                emp.ProfesorId == profesorId);
        }

        public async Task<IEnumerable<EstudianteMateriaProfesor>> GetMateriasDelEstudianteAsync(int estudianteId)
        {
            return await _context.EstudianteMateriaProfesor
                .Include(emp => emp.Materia)
                .Include(emp => emp.Profesor)
                .Where(emp => emp.EstudianteId == estudianteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EstudianteMateriaProfesor>> GetEstudiantesPorMateriaProfesorAsync(int materiaId, int profesorId)
        {
            return await _context.EstudianteMateriaProfesor
                .Include(emp => emp.Estudiante)
                .Where(emp => emp.MateriaId == materiaId && emp.ProfesorId == profesorId)
                .ToListAsync();
        }

        public async Task<bool> EstudianteTieneClaseConProfesorAsync(int estudianteId, int profesorId)
        {
            return await _context.EstudianteMateriaProfesor
                .AnyAsync(emp => emp.EstudianteId == estudianteId && emp.ProfesorId == profesorId);
        }

        public async Task<int> ContarMateriasDelEstudianteAsync(int estudianteId)
        {
            return await _context.EstudianteMateriaProfesor
                .CountAsync(emp => emp.EstudianteId == estudianteId);
        }

        public async Task<IEnumerable<EstudianteMateriaProfesor>> GetMateriasPorEstudianteAsync(int estudianteId)
        {
            return await _context.EstudianteMateriaProfesor
                .Include(emp => emp.Materia)
                .Where(emp => emp.EstudianteId == estudianteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EstudianteMateriaProfesor>> GetProfesoresPorEstudianteAsync(int estudianteId)
        {
            return await _context.EstudianteMateriaProfesor
                .Include(emp => emp.Profesor)
                .Where(emp => emp.EstudianteId == estudianteId)
                .ToListAsync();
        }
    }
}

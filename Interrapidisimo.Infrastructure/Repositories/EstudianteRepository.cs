using Microsoft.EntityFrameworkCore;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;
using Interrapidisimo.Infrastructure.Data;

namespace Interrapidisimo.Infrastructure.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly ApplicationDbContext _context;

        public EstudianteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            return await _context.Estudiantes.FindAsync(id);
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante> AddAsync(Estudiante estudiante)
        {
            await _context.Estudiantes.AddAsync(estudiante);
            return estudiante;
        }

        public void Update(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
        }

        public void Delete(Estudiante estudiante)
        {
            _context.Estudiantes.Remove(estudiante);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Estudiantes.AnyAsync(e => e.Id == id);
        }

        public async Task<Estudiante?> GetEstudianteWithMateriasAsync(int estudianteId)
        {
            return await _context.Estudiantes
                .Include(e => e.EstudianteMateriaProfesor)
                    .ThenInclude(emp => emp.Materia)
                .Include(e => e.EstudianteMateriaProfesor)
                    .ThenInclude(emp => emp.Profesor)
                .FirstOrDefaultAsync(e => e.Id == estudianteId);
        }

        public async Task<IEnumerable<Estudiante>> GetCompanerosDeClaseAsync(int estudianteId, int materiaId)
        {
            // Obtener todos los estudiantes inscritos en la misma materia, excluyendo al estudiante actual
            return await _context.Estudiantes
                .Where(e => e.Id != estudianteId && 
                           e.EstudianteMateriaProfesor.Any(emp => emp.MateriaId == materiaId))
                .ToListAsync();
        }

        public async Task<bool> TieneClaseConProfesorAsync(int estudianteId, int profesorId)
        {
            return await _context.EstudianteMateriaProfesor
                .AnyAsync(emp => emp.EstudianteId == estudianteId && emp.ProfesorId == profesorId);
        }

        public async Task<int> GetCreditosSeleccionadosAsync(int estudianteId)
        {
            return await _context.EstudianteMateriaProfesor
                .Where(emp => emp.EstudianteId == estudianteId)
                .SumAsync(emp => emp.Materia.Creditos);
        }

        public async Task<IEnumerable<Estudiante>> GetEstudiantesPorMateriaAsync(int materiaId)
        {
            return await _context.Estudiantes
                .Where(e => e.EstudianteMateriaProfesor.Any(emp => emp.MateriaId == materiaId))
                .ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;
using Interrapidisimo.Infrastructure.Data;

namespace Interrapidisimo.Infrastructure.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly ApplicationDbContext _context;

        public MateriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Materia?> GetByIdAsync(int id)
        {
            return await _context.Materias.FindAsync(id);
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await _context.Materias.ToListAsync();
        }

        public async Task<Materia> AddAsync(Materia materia)
        {
            await _context.Materias.AddAsync(materia);
            return materia;
        }

        public void Update(Materia materia)
        {
            _context.Materias.Update(materia);
        }

        public void Delete(Materia materia)
        {
            _context.Materias.Remove(materia);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Materias.AnyAsync(m => m.Id == id);
        }

        public async Task<Materia?> GetMateriaWithProfesoresAsync(int materiaId)
        {
            return await _context.Materias
                .Include(m => m.MateriaProfesor)
                    .ThenInclude(mp => mp.Profesor)
                .FirstOrDefaultAsync(m => m.Id == materiaId);
        }

        public async Task<IEnumerable<Materia>> GetMateriasDisponiblesAsync()
        {
            return await _context.Materias
                .Where(m => m.MateriaProfesor.Any()) // Solo materias que tienen profesores asignados
                .ToListAsync();
        }

        public async Task<IEnumerable<Materia>> GetMateriasByProfesorAsync(int profesorId)
        {
            return await _context.Materias
                .Where(m => m.MateriaProfesor.Any(mp => mp.ProfesorId == profesorId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Materia>> GetMateriasDisponiblesParaEstudianteAsync(int estudianteId)
        {
            // Obtener materias que tienen profesores asignados y en las que el estudiante no está inscrito
            return await _context.Materias
                .Where(m => m.MateriaProfesor.Any() && 
                           !m.EstudianteMateriaProfesor.Any(emp => emp.EstudianteId == estudianteId))
                .ToListAsync();
        }
    }
}

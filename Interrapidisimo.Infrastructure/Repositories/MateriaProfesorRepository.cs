using Microsoft.EntityFrameworkCore;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;
using Interrapidisimo.Infrastructure.Data;

namespace Interrapidisimo.Infrastructure.Repositories
{
    public class MateriaProfesorRepository : IMateriaProfesorRepository
    {
        private readonly ApplicationDbContext _context;

        public MateriaProfesorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MateriaProfesor?> GetMateriaProfesorAsync(int materiaId, int profesorId)
        {
            return await _context.MateriaProfesor
                .Include(mp => mp.Materia)
                .Include(mp => mp.Profesor)
                .FirstOrDefaultAsync(mp => mp.MateriaId == materiaId && mp.ProfesorId == profesorId);
        }

        public async Task<MateriaProfesor> AddAsync(MateriaProfesor materiaProfesor)
        {
            await _context.MateriaProfesor.AddAsync(materiaProfesor);
            return materiaProfesor;
        }

        public void Delete(MateriaProfesor materiaProfesor)
        {
            _context.MateriaProfesor.Remove(materiaProfesor);
        }

        public async Task<IEnumerable<MateriaProfesor>> GetMateriasPorProfesorAsync(int profesorId)
        {
            return await _context.MateriaProfesor
                .Include(mp => mp.Materia)
                .Where(mp => mp.ProfesorId == profesorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MateriaProfesor>> GetProfesoresPorMateriaAsync(int materiaId)
        {
            return await _context.MateriaProfesor
                .Include(mp => mp.Profesor)
                .Where(mp => mp.MateriaId == materiaId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int materiaId, int profesorId)
        {
            return await _context.MateriaProfesor
                .AnyAsync(mp => mp.MateriaId == materiaId && mp.ProfesorId == profesorId);
        }
    }
}

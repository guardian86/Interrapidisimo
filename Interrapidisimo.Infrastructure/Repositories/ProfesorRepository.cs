using Microsoft.EntityFrameworkCore;
using Interrapidisimo.Domain.Entities;
using Interrapidisimo.Domain.Interfaces;
using Interrapidisimo.Infrastructure.Data;

namespace Interrapidisimo.Infrastructure.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfesorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Profesor?> GetByIdAsync(int id)
        {
            return await _context.Profesores.FindAsync(id);
        }

        public async Task<IEnumerable<Profesor>> GetAllAsync()
        {
            return await _context.Profesores.ToListAsync();
        }

        public async Task<Profesor> AddAsync(Profesor profesor)
        {
            await _context.Profesores.AddAsync(profesor);
            return profesor;
        }

        public void Update(Profesor profesor)
        {
            _context.Profesores.Update(profesor);
        }

        public void Delete(Profesor profesor)
        {
            _context.Profesores.Remove(profesor);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Profesores.AnyAsync(p => p.Id == id);
        }

        public async Task<Profesor?> GetProfesorWithMateriasAsync(int profesorId)
        {
            return await _context.Profesores
                .Include(p => p.MateriaProfesor)
                    .ThenInclude(mp => mp.Materia)
                .FirstOrDefaultAsync(p => p.Id == profesorId);
        }

        public async Task<IEnumerable<Profesor>> GetProfesoresConMenosDeDosMateriasAsync()
        {
            return await _context.Profesores
                .Where(p => p.MateriaProfesor.Count < 2)
                .ToListAsync();
        }

        public async Task<bool> DictaExactamenteDosMateriasAsync(int profesorId)
        {
            var materiaCount = await _context.MateriaProfesor
                .CountAsync(mp => mp.ProfesorId == profesorId);
            return materiaCount == 2;
        }
    }
}

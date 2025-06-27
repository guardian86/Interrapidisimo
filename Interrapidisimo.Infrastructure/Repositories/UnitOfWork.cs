using Microsoft.EntityFrameworkCore.Storage;
using Interrapidisimo.Domain.Interfaces;
using Interrapidisimo.Infrastructure.Data;
using Interrapidisimo.Infrastructure.Repositories;

namespace Interrapidisimo.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            EstudianteRepository = new EstudianteRepository(_context);
            ProfesorRepository = new ProfesorRepository(_context);
            MateriaRepository = new MateriaRepository(_context);
            MateriaProfesorRepository = new MateriaProfesorRepository(_context);
            EstudianteMateriaProfesorRepository = new EstudianteMateriaProfesorRepository(_context);
        }

        public IEstudianteRepository EstudianteRepository { get; }
        public IProfesorRepository ProfesorRepository { get; }
        public IMateriaRepository MateriaRepository { get; }
        public IMateriaProfesorRepository MateriaProfesorRepository { get; }
        public IEstudianteMateriaProfesorRepository EstudianteMateriaProfesorRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}

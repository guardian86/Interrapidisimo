namespace Interrapidisimo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEstudianteRepository EstudianteRepository { get; }
        IProfesorRepository ProfesorRepository { get; }
        IMateriaRepository MateriaRepository { get; }
        IMateriaProfesorRepository MateriaProfesorRepository { get; }
        IEstudianteMateriaProfesorRepository EstudianteMateriaProfesorRepository { get; }
        
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

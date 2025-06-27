namespace Interrapidisimo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEstudianteRepository Estudiantes { get; }
        IProfesorRepository Profesores { get; }
        IMateriaRepository Materias { get; }
        IMateriaProfesorRepository MateriasProfesores { get; }
        IEstudianteMateriaProfesorRepository EstudiantesMateriasProfesores { get; }
        
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

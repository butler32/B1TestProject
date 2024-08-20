using B1TestProject.Domain.Entities;

namespace B1TestProject.Infrastructure.Interfaces
{
    public interface IRepositoryBase<T> 
        where T : BaseEntity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<T?> Get(Guid id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAllAsync(CancellationToken cancellationToken);
    }
}

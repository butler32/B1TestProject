using B1TestProject.Domain.Entities;

namespace B1TestProject.Infrastructure.Interfaces
{
    public interface ITextRepository <T> : IRepositoryBase<T>
        where T : TextLineEntity
    {
        Task InsertButchAsync(List<T> entities, CancellationToken cancellationToken);
    }
}

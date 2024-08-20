using B1TestProject.Domain.Entities;

namespace B1TestProject.Infrastructure.Interfaces
{
    public interface IExcelEntryRepository<T> : IRepositoryBase<T>
        where T : BalanceSheetEntryEntity
    {
        
    }
}

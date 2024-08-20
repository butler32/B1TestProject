using B1TestProject.Domain.Entities;

namespace B1TestProject.Infrastructure.Interfaces
{
    public interface IExcelFileRepository<T> : IRepositoryBase<T>
        where T : ExcelFilesEntity
    {
        
    }
}

using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace B1TestProject.Infrastructure.Repositories
{
    public class ExcelEntryRepository<T> : RepositoryBase<T>, IExcelEntryRepository<T>
        where T : BalanceSheetEntryEntity
    {
        public ExcelEntryRepository(TestProjDbContext context) : base(context)
        {
            
        }

        
    }
}

using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace B1TestProject.Infrastructure.Repositories
{
    public class ExcelFileRepository<T> : RepositoryBase<T>, IExcelFileRepository<T>
        where T : ExcelFilesEntity
    {
        public ExcelFileRepository(TestProjDbContext context) : base(context)
        {
            
        }

        public new async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().Include(x => x.BalanceSheetEntries).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}

using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace B1TestProject.Infrastructure.Repositories
{
    public class TextRepository<T> : RepositoryBase<T>, ITextRepository<T>
        where T : TextLineEntity
    {
        public TextRepository(TestProjDbContext context) : base(context)
        {
            
        }

        public async Task InsertButchAsync(List<T> entities, CancellationToken cancellationToken)
        {
            await _context.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _context.ChangeTracker.Clear();
        }
    }
}

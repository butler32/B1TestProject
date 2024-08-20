using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace B1TestProject.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : BaseEntity
    {
        protected readonly TestProjDbContext _context;

        public RepositoryBase(TestProjDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _context.ChangeTracker.Clear();

            return entity;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<T?> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync((i => i.Id == id), cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task DeleteAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
            _context.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

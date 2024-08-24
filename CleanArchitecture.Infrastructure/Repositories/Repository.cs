using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitecture.Infrastructure.Repositories
{
    internal abstract class Repository<TEntity,TEntityId>
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {

        protected readonly ApplicationDbContext _context;

        protected Repository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default!)
        {

            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }


    }
}

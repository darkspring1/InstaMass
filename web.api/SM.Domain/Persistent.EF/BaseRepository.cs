using Microsoft.EntityFrameworkCore;
using SM.Common.Cache;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Domain.Persistent.EF
{
    public class BaseRepository<TEntity> : CacheRepository, IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbSet<TEntity> Set;

        public BaseRepository(ICacheProvider cacheProvider, DataContext context) : base(cacheProvider, context)
        {
            Set = context.Set<TEntity>();
        }


        IQueryable<TEntity> Queryable(ISpecification<TEntity> specification)
        {
            return specification.Build(Set);
        }

        public void Add(params TEntity[] entities)
        {
            Set.AddRange(entities);
        }

        public Task<TEntity> FirstAsync(ISpecification<TEntity> specification)
        {
            return Queryable(specification).FirstAsync();
        }

        public Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification)
        {
            return Queryable(specification).FirstOrDefaultAsync();
        }

        public Task<TEntity[]> GetItemsAsync(ISpecification<TEntity> specification)
        {
            return Queryable(specification).ToArrayAsync();
        }

        public Task<TEntity[]> GetItemsAsync()
        {
            return Set.ToArrayAsync();
        }
    }
}
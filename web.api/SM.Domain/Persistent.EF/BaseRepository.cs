using Microsoft.EntityFrameworkCore;
using SM.Common.Cache;

namespace SM.Domain.Persistent.EF
{
    public abstract class BaseRepository<TEntity> : CacheRepository
        where TEntity : class
    {
        protected readonly DbSet<TEntity> Set;

        public BaseRepository(ICacheProvider cacheProvider, DataContext context) : base(cacheProvider, context)
        {
            Set = context.Set<TEntity>();
        }
    }
}
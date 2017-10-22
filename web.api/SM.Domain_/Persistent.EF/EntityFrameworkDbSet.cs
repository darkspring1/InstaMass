using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Domain.Persistent.EF
{
    class EntityFrameworkDbSet<T> : IEntityFrameworkDbSet<T>  where T : class
    {
        private readonly DbSet<T> _set;
        public EntityFrameworkDbSet(DbSet<T> set)
        {
            _set = set;
        }

        public IQueryable<T> Entities => _set;

        public T Add(T entity)
        {
            return _set.Add(entity).Entity;
        }

        public T Remove(T entity)
        {
            return _set.Remove(entity).Entity;
        }

        public T Find(params object[] keyValues)
        {
            return _set.Find(keyValues);
        }

        public Task<T> FindAsync(params object[] keyValues)
        {
            return _set.FindAsync(keyValues);
        }
    }
}
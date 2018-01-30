using System.Linq;
using System.Threading.Tasks;

namespace SM.Domain.Persistent.EF
{
    public interface IEntityFrameworkDbSet<T>
    {
        IQueryable<T> Entities { get; }

        T Add(T entity);

        T Find(params object[] keyValues);

        Task<T> FindAsync(params object[] keyValues);

        void Remove(T entity);

        void RemoveRange(params T[] entities);

    }
}
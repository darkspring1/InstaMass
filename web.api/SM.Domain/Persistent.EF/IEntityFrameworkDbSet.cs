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

        T Remove(T entity);
    }
}
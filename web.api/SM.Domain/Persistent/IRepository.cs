using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IRepository<T>
    {
        Task<T[]> GetItemsAsync(ISpecification<T> specification);

        Task<T> FirstAsync(ISpecification<T> specification);

        Task<T> FirstOrDefaultAsync(ISpecification<T> specification);

        void Add(T entity);
    }
}

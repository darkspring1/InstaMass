using System.Linq;

namespace SM.Domain.Persistent
{
    public interface ISpecification<T>
    {
        IQueryable<T> Build(IQueryable<T> source);
    }
}

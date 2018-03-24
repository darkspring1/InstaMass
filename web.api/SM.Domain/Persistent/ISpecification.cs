using System;
using System.Linq;
using System.Linq.Expressions;

namespace SM.Domain.Persistent
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfiedBy();

        IQueryable<T> Include(IQueryable<T> source);
    }
}

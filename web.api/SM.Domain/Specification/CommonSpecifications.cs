using SM.Domain.Model;
using SM.Domain.Persistent;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SM.Domain.Specification
{
    public static class CommonSpecifications
    {
        public static ISpecification<T> GetById<T, TKey>(TKey id)
            where T : IEntity<TKey>
        {
            return new GetByIdSpecification_<T, TKey>(id);
        }
    }

    public class GetByIdSpecification_<T, TKey> : ISpecification<T>
       where T : IEntity<TKey>
    {
        private readonly TKey _id;

        public GetByIdSpecification_(TKey id)
        {
            _id = id;
        }

        public virtual IQueryable<T> Include(IQueryable<T> source)
        {
            return source;
        }

        public Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return t => t.Id.Equals(_id);
        }
    }

}

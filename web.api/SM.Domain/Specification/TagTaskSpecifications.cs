using Microsoft.EntityFrameworkCore;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SM.Domain.Specification
{
    public static class TagTaskSpecifications
    {
        public static ISpecification<TagTask> GetById(Guid id)
        {
            return new GetByIdSpecification(id);
        }
    }


    class GetByIdSpecification : ISpecification<TagTask>
    {
        private readonly Guid _id;

        public GetByIdSpecification(Guid id)
        {
            _id = id;
        }

        public IQueryable<TagTask> Include(IQueryable<TagTask> source)
        {
            return source.Include(tt => tt.Task);
        }

        public Expression<Func<TagTask, bool>> IsSatisfiedBy()
        {
            return tt => tt.TaskId == _id;
        }
    }
}

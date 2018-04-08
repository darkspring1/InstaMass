using Microsoft.EntityFrameworkCore;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System;
using System.Linq;

namespace SM.Domain.Specification
{
    public static class TagTaskSpecifications
    {
        public static ISpecification<TagTask> GetById(Guid id)
        {
            return new GetByIdSpecification(id);
        }

        public static ISpecification<TagTask> WithAccount()
        {
            return new WithAccountSpecification();
        }

        class WithAccountSpecification : ISpecification<TagTask>
        {
            public IQueryable<TagTask> Build(IQueryable<TagTask> source)
            {
                return source
                    .Include(tt => tt.Task)
                    .ThenInclude(tt => tt.Account);
            }
        }

        class GetByIdSpecification : ISpecification<TagTask>
        {
            private readonly Guid _id;

            public GetByIdSpecification(Guid id)
            {
                _id = id;
            }

            public IQueryable<TagTask> Build(IQueryable<TagTask> source)
            {
                return source
                    .Where(tt => tt.TaskId == _id)
                    .Include(tt => tt.Task);
            }
        }

    }
}

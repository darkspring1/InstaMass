using Microsoft.EntityFrameworkCore;
using SM.Domain.ConstantValues;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SM.Domain.Specification
{
    public static class SMTaskSpecifications
    {
        public static ISpecification<SMTask> GetActiveTasksByUserId(Guid userId)
        {
            return new GetActiveTasksByUserId(userId);
        }
    }

    public class GetActiveTasksByUserId : ISpecification<SMTask>
    {
        private readonly Guid _userId;

        public GetActiveTasksByUserId(Guid userId)
        {
            _userId = userId;
        }

        public IQueryable<SMTask> Include(IQueryable<SMTask> source)
        {
            return source.Include(t => t.Account);
        }

        public Expression<Func<SMTask, bool>> IsSatisfiedBy()
        {
            return t => t.EntityStatusId == EntityStatuses.Active && t.Account.UserId == _userId;
        }
    }


}

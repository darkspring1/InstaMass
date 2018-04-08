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

        public IQueryable<SMTask> Build(IQueryable<SMTask> source)
        {
            return source
                .Where(t => t.EntityStatusId == EntityStatuses.Active && t.Account.UserId == _userId)
                .Include(t => t.Account);
        }
    }


}

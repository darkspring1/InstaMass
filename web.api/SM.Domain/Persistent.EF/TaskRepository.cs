using SM.Domain.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;
using Microsoft.EntityFrameworkCore;

namespace SM.Domain.Persistent.EF
{
    class TaskRepository :  BaseRepository<SMTask, SMTask>, ITaskRepository
    {
        public TaskRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        public Task<SMTask[]> GetByUserAsync(Guid userId)
        {
            var taskStates =  Set.Entities
                .Where(t => t.Account.UserId == userId)
                .Include(t => t.Account)
                .ToArrayAsync();
            return CreateArrayAsync(taskStates);

        }

        protected override SMTask Create(SMTask state)
        {
            return state;
        }
    }
}

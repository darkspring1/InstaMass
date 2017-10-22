using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System;
using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;
using System.Data.Entity;

namespace SM.Domain.Persistent.EF
{
    class TaskRepository :  BaseRepository<SMTask, TaskState>, ITaskRepository
    {
        public TaskRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        public Task<SMTask[]> GetByUserAsync(Guid userId)
        {
            var taskStates =  Set.Entities.Where(t => t.Account.UserId == userId).ToArrayAsync();
            return CreateArrayAsync(taskStates);

        }

        protected override SMTask Create(TaskState state)
        {
            return new SMTask(state);
        }
    }
}

using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System;
using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;

namespace SM.Domain.Persistent.EF
{
    class LikeTaskRepository :  BaseRepository<LikeTask, LikeTaskState>, ILikeTaskRepository
    {
        public LikeTaskRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        public void AddLikeTask(LikeTask task)
        {
            Set.Add(task.State);
        }

        public Task<LikeTask> GetLikeTaskAsync(Guid userId, Guid taskId)
        {
            var tasks = Set.Entities.Where(lt => lt.TaskId == taskId && lt.Task.Account.UserId == userId);
            return CreateAsync(FirstOrDefaultAsync(tasks));
        }

        protected override LikeTask Create(LikeTaskState state)
        {
            return new LikeTask(state);
        }
    }
}

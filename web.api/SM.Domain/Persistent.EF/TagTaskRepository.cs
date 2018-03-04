using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System;
using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;

namespace SM.Domain.Persistent.EF
{
    class TagTaskRepository :  BaseRepository<TagTask, TagTask>, ITagTaskRepository
    {
        public TagTaskRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        public void AddTagTask(TagTask task)
        {
            Set.Add(task);
        }

        public Task<TagTask> GetTagTaskByIdAsync(Guid taskId)
        {
            var tasks = Set.Entities.Where(lt => lt.TaskId == taskId);
            return CreateAsync(FirstOrDefaultAsync(tasks));
        }

        protected override TagTask Create(TagTask state)
        {
            return state;
        }
    }
}

using SM.Domain.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;
using Microsoft.EntityFrameworkCore;

namespace SM.Domain.Persistent.EF
{
    class TagTaskRepository :  BaseRepository<TagTask>, ITagTaskRepository
    {
        public TagTaskRepository(ICacheProvider cacheProvider, DataContext context) : base(cacheProvider, context)
        {
        }

        public void AddTagTask(TagTask task)
        {
            Set.Add(task);
        }

        public Task<TagTask> GetTagTaskByIdAsync(Guid taskId)
        {
            return Set.Where(lt => lt.TaskId == taskId).FirstOrDefaultAsync();
        }
    }
}

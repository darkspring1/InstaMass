using SM.Domain.Model;
using System;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface ITagTaskRepository
    {
        Task<TagTask> GetTagTaskByIdAsync(Guid taskId);

        void AddLikeTask(TagTask task);
    }
}

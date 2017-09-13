using SM.Domain.Model;
using System;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface ILikeTaskRepository
    {
        Task<LikeTask> GetLikeTaskAsync(Guid userId, Guid taskId);

        void AddLikeTask(LikeTask task);
    }
}

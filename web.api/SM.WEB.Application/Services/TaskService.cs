using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Events;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System;
using System.Threading.Tasks;

namespace SM.WEB.Application.Services
{
    public class TaskService : SMBaseService
    {
        public TaskService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(unitOfWork, logger, eventDispatcher)
        {
        }

        public Task<ServiceResult<SMTask[]>> GetTasks(Guid userId)
        {
            return RunAsync(() => UnitOfWork.TaskRepository.GetByUserAsync(userId));
        }

        public Task<ServiceResult<TagTask>> CreateLikeTask(Guid accountId, string[] tags)
        {
            return RunAsync(async () => {
                TagTask task = TagTask.Create(accountId, tags);
                UnitOfWork.LikeTaskRepository.AddLikeTask(task);
                await UnitOfWork.CompleteAsync();
                await RaiseAsync(new TagTaskWasCreated { Task = task });
                return task;
            });
        }
    }
}

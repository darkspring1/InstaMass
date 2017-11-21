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

        public Task<ServiceResult<TagTask>> CreateTagTask(Guid accountId, string[] tags)
        {
            return RunAsync(async () =>
            {
                TagTask task = TagTask.Create(accountId, tags);
                UnitOfWork.TagTaskRepository.AddTagTask(task);
                var completeTask = UnitOfWork.CompleteAsync();

                Task<Account> accountTask;
                //2 паралельных запроса
                using (var unitOfWork2 = UnitOfWork.CreateNewInstance())
                {
                    accountTask = unitOfWork2.AccountRepository.GetByIdAsync(accountId);
                    await Task.WhenAll(accountTask, completeTask);
                }
                
                await RaiseAsync(new TagTaskWasCreated(accountTask.Result.Login, task));
                return task;
            });
        }
    }
}

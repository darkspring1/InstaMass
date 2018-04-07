using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Dto.TagTask;
using SM.Domain.Events;
using SM.Domain.Model;
using SM.Domain.Persistent;
using SM.Domain.Specification;
using System;
using System.Threading.Tasks;

namespace SM.Domain.Services
{
    public class TaskService : DomainService
    {
        public TaskService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(unitOfWork, logger, eventDispatcher)
        {
        }

        public Task<ServiceResult<SMTask[]>> GetTasks(Guid userId)
        {
            return RunAsync(() => UnitOfWork.TaskRepository.GetItemsAsync(SMTaskSpecifications.GetActiveTasksByUserId(userId)));
        }
        public Task<ServiceResult> DeleteTaskAsync(Guid taskId)
        {
            return RunAsync(async () =>
            {
                var task = await UnitOfWork.TaskRepository.FirstAsync(CommonSpecifications.GetById<SMTask, Guid>(taskId));

                task.MarkAsDeleted();
                await UnitOfWork.CompleteAsync();

                //Task<Account> accountTask;
                ////2 паралельных запроса
                //var unitOfWork2 = UnitOfWork.CreateNewInstance();
                //accountTask = unitOfWork2.AccountRepository.GetByIdAsync(dto.AccountId);
                //await Task.WhenAll(accountTask, completeTask);
                //await RaiseAsync(new TagTaskWasCreatedOrUpdated(accountTask.Result.Login, task));
                //return task;
            });
        }

    }
}

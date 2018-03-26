using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Dto.TagTask;
using SM.Domain.Events;
using SM.Domain.Model;
using SM.Domain.Persistent;
using SM.Domain.Specification;
using System;
using System.Threading.Tasks;

namespace SM.WEB.Application.Services
{
    public class TaskService : ApplicationService
    {
        public TaskService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(unitOfWork, logger, eventDispatcher)
        {
        }

        public Task<ServiceResult<SMTask[]>> GetTasks(Guid userId)
        {
            return RunAsync(() => UnitOfWork.TaskRepository.GetItemsAsync(SMTaskSpecifications.GetActiveTasksByUserId(userId)));
        }

        public Task<ServiceResult<TagTask>> CreateTagTaskAsync(TagTaskDto dto)
        {
            return RunAsync(async () =>
            {
                TagTask task = TagTask.Create(dto);

                UnitOfWork.TagTaskRepository.Add(task);
                var completeTask = UnitOfWork.CompleteAsync();

                Task<Account> accountTask;
                //2 паралельных запроса
                var unitOfWork2 = UnitOfWork.CreateNewInstance();
                accountTask = unitOfWork2.AccountRepository.GetByIdAsync(dto.AccountId);
                await Task.WhenAll(accountTask, completeTask);
                await RaiseAsync(new TagTaskWasCreatedOrUpdated(accountTask.Result.Login, task));
                return task;
            });
        }

        public Task<ServiceResult<TagTask>> UpdateTagTaskAsync(Guid taskId,TagTaskDto dto)
        {
            return RunAsync(async () =>
            {
                var task = await UnitOfWork.TagTaskRepository.FirstAsync(TagTaskSpecifications.GetById(taskId));

                task.Update(dto);
                var completeTask = UnitOfWork.CompleteAsync();
                //todo: подумать как хранить логин вместе с задачей, что бы экономить на join'ах
                Task<Account> accountTask;
                //2 паралельных запроса
                var unitOfWork2 = UnitOfWork.CreateNewInstance();
                accountTask = unitOfWork2.AccountRepository.GetByIdAsync(dto.AccountId);
                await Task.WhenAll(accountTask, completeTask);
                await RaiseAsync(new TagTaskWasCreatedOrUpdated(accountTask.Result.Login, task));
                return task;
            });
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


        public Task<ServiceResult<TagTask>> GetTagTaskAsync(Guid taskId)
        {
            return RunAsync(() => UnitOfWork.TagTaskRepository.FirstAsync(TagTaskSpecifications.GetById(taskId)));
        }
    }
}

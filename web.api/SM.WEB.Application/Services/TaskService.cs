using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System;
using System.Threading.Tasks;

namespace SM.WEB.Application.Services
{

    public class TaskService : BaseService
    {
        

        private IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ServiceResult<SMTask[]>> GetTasks(Guid userId)
        {
            return RunAsync(() => _unitOfWork.TaskRepository.GetByUserAsync(userId));
        }

        public Task<ServiceResult<LikeTask>> CreateLikeTask(Guid accountId, string[] tags)
        {
            return RunAsync(async () => {
                LikeTask task = LikeTask.Create(accountId, tags);
                _unitOfWork.LikeTaskRepository.AddLikeTask(task);
                await _unitOfWork.CompleteAsync();
                return task;
            });
        }


    }
}

using SM.Common.Log;
using SM.Domain.Model;
using SM.WEB.API.Models;
using SM.WEB.Application.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace SM.WEB.API.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        private readonly Func<TaskService> _taskServiceServiceFunc;

        public TaskController(ILogger logger, Func<TaskService> taskServiceServiceFunc) : base(logger)
        {
            _taskServiceServiceFunc = taskServiceServiceFunc;
        }

        [HttpGet]
        [Route(Routes.ApiTasks)]
        public Task<IHttpActionResult> GetTasks()
        {
            return ActionResultAsync(_taskServiceServiceFunc().GetTasks(UserId));
        }


        [HttpPost]
        [Route(Routes.ApiTaskLike)]
        public Task<IHttpActionResult> CreateLikeTask(NewLikeTaskModel model)
        {
            return ActionResultAsync(_taskServiceServiceFunc().CreateLikeTask(model.AccountId, model.Tags));
        }

    }
    
}

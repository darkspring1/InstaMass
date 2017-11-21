using SM.Common.Log;
using SM.WEB.API.Models;
using SM.WEB.Application.Services;
using System;
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
        [Route(Routes.ApiTaskTag)]
        public Task<IHttpActionResult> CreateTagTask(NewTagTaskModel model)
        {
            return ActionResultAsync(_taskServiceServiceFunc().CreateTagTask(model.AccountId, model.Tags));
        }

    }
    
}

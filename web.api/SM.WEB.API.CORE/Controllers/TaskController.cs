using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Common.Log;
using SM.Domain.Dto.TagTask;
using SM.Domain.Model;
using SM.Domain.Services;
using System;
using System.Threading.Tasks;

namespace SM.WEB.API.Controllers
{
    [Authorize]
    [Route("api/")]
    public class TaskController : BaseController
    {
        private readonly TaskService _taskServiceService;

        public TaskController(ILogger logger, TaskService taskServiceService) : base(logger)
        {
            _taskServiceService = taskServiceService;
        }

        [HttpGet]
        [Route(Routes.Tasks_Get)]
        public Task<ActionResult> GetTasks()
        {
            return ActionResultAsync(_taskServiceService.GetTasks(UserId));
        }


        [HttpPost(Routes.TagTask_Post)]
        public Task<ActionResult> TagTaskPost([FromBody]TagTaskDto model)
        {
            var lastPost = new SwitchedProperty(model.LastPost.Value, model.LastPost.Disabled);
            var posts = FromModel(model.Posts);
            var followers = FromModel(model.Followers);
            var followings = FromModel(model.Followings);
            return ActionResultAsync(_taskServiceService.CreateTagTaskAsync(model));
        }

        [HttpGet(Routes.TagTask_Get)]
        public Task<ActionResult> TagTaskGet(Guid id)
        {
            return ActionResultAsync(_taskServiceService.GetTagTaskAsync(id));
        }

        [HttpPut(Routes.TagTask_Put)]
        public Task<ActionResult> TagTaskPut(Guid id, [FromBody]TagTaskDto model)
        {
            return ActionResultAsync(_taskServiceService.UpdateTagTaskAsync(id, model));
        }


        [HttpDelete(Routes.Tasks_Delete)]
        public Task<ActionResult> TaskDelte(Guid id)
        {
            return ActionResultAsync(_taskServiceService.DeleteTaskAsync(id));
        }

        SwitchedRange FromModel(SwitchedRangeDto model) {
            return new SwitchedRange(model.From, model.To, model.Disabled);
        }
    }
    
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Common.Log;
using SM.Domain.Model;
using SM.WEB.API.Models;
using SM.WEB.Application.Services;
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
        public Task<ActionResult> TagTaskPost([FromBody]NewTagTaskModel model)
        {
            var lastPost = new SwitchedProperty(model.LastPost.Value, model.LastPost.Disabled);
            var posts = FromModel(model.Posts);
            var followers = FromModel(model.Followers);
            var followings = FromModel(model.Followings);
            return ActionResultAsync(_taskServiceService.CreateTagTaskAsync(model.AccountId, model.Tags, model.AvatarExistDisabled, lastPost, posts: posts, followers: followers, followings: followings));
        }

        [HttpGet(Routes.TagTask_Get)]
        public Task<ActionResult> TagTaskGet(Guid id)
        {
            return ActionResultAsync(_taskServiceService.GetTagTaskAsync(id));
        }


        SwitchedRange FromModel(SwitchedRangeModel model) {
            return new SwitchedRange(model.From, model.To, model.Disabled);
        }


    }
    
}
